using System.Net;

namespace WebAPI.Core.Common.Exceptions;

public class IncorrectPasswordException : BaseException
{
    public IncorrectPasswordException(string? message = "Incorrect password has been entered.") : base(message)
    {
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}