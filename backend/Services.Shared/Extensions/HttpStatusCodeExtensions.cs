using System.Net;
using Services.Shared.Core.Exceptions;

namespace Services.Shared.Extensions;

public static class HttpStatusCodeExtensions
{
    public static void RaiseExceptionOnFailure(this HttpStatusCode httpStatusCode)
    {
        if (400 > (int)httpStatusCode) return;

        throw httpStatusCode switch
        {
            HttpStatusCode.NotFound => new NotFoundException(),
            _ => new ExternalServiceException()
        };
    }
}