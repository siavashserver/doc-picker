using System.Net;
using Grpc.Core;

namespace Services.Shared.Core.Exceptions;

public class ExternalServiceException : BaseException
{
    public ExternalServiceException(string? message = "External service failure.") : base(message)
    {
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.InternalServerError;
    }

    public override StatusCode GetGrpcStatusCode()
    {
        return StatusCode.Internal;
    }
}