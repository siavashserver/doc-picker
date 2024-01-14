using System.Text.Json;
using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Services.Shared.Core.Exceptions;

namespace Services.Shared.WebAPI;

public class ExceptionInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(validationFailure => validationFailure.PropertyName,
                    (key, validationFailures) => new
                    {
                        PropertyName = key,
                        Errors = validationFailures.Select(validationFailure => validationFailure.ErrorMessage)
                    }
                );
            var serializedError = JsonSerializer.Serialize(errors);

            throw new RpcException(new Status(StatusCode.InvalidArgument, serializedError));
        }
        catch (BaseException baseException)
        {
            throw new RpcException(new Status(baseException.GetGrpcStatusCode(), baseException.Message));
        }
        catch (Exception exception)
        {
            throw new RpcException(new Status(StatusCode.Unknown, exception.Message));
        }
    }
}