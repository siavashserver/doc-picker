using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Shared.WebAPI;

public class RequestValidationInterceptor(
    IServiceProvider serviceProvider
) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var validator = serviceProvider.GetService<IValidator<TRequest>>();
        await validator.ValidateAndThrowAsync(request);

        return await continuation(request, context);
    }
}