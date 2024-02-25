using System.Net;
using Grpc.Core;

namespace Services.Shared.Core.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string? message = null) : base(message)
    {
    }

    public abstract HttpStatusCode GetHttpStatusCode();
    public abstract StatusCode GetGrpcStatusCode();
}