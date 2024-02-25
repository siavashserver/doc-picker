using System.Net;
using Grpc.Core;

namespace Services.Shared.Core.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string? message = "Record not found.") : base(message)
    {
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.NotFound;
    }

    public override StatusCode GetGrpcStatusCode()
    {
        return StatusCode.NotFound;
    }
}