namespace Services.Shared.Core.Interfaces;

public interface IRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request);
}