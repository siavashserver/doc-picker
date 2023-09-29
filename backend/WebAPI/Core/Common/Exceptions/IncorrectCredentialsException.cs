using System.Net;

namespace WebAPI.Core.Common.Exceptions;

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
}