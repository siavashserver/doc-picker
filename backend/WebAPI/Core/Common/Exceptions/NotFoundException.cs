using System.Net;

namespace WebAPI.Core.Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string? message = "Record not found.") : base(message)
    {
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.NotFound;
    }
}