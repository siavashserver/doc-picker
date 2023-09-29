using System.Net;

namespace WebAPI.Core.Common.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string? message = null) : base(message)
    {
    }

    public abstract HttpStatusCode GetHttpStatusCode();
}