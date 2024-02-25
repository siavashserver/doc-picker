using System.Net;
using Grpc.Core;
using Services.Shared.Core.Exceptions;

namespace Services.Accounts.Core.Exceptions;

public class IncorrectPasswordException : BaseException
{
    public IncorrectPasswordException(string? message = "Incorrect password has been entered.") : base(message)
    {
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }

    public override StatusCode GetGrpcStatusCode()
    {
        return StatusCode.InvalidArgument;
    }
}