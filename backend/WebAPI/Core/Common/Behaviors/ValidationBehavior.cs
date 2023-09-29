using FluentValidation;
using MediatR;

namespace WebAPI.Core.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator) :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        return await next();
    }
}