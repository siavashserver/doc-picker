using System.Net;
using Grpc.Core;
using Services.Shared.Core.Exceptions;

namespace Services.Accounts.Core.Exceptions;

public class IncorrectCredentialsException : BaseException
{
    public IncorrectCredentialsException(string? message = "Incorrect login credentials has been entered.") :
        base(message)
    {
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }

    public override StatusCode GetGrpcStatusCode()
    {
        return StatusCode.Unauthenticated;
    }
}