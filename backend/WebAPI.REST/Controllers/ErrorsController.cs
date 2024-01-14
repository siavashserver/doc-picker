using System.Net;
using System.Text.Json;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.REST.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : BaseAPIController
{
    [AllowAnonymous]
    public ActionResult ErrorHandler()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return exception switch
        {
            //BaseException exc => Problem(statusCode: (int)exc.GetHttpStatusCode(), detail: exc.Message),
            RpcException exc => HandleRpcExceptions(exc),
            _ => Problem(statusCode: (int)HttpStatusCode.InternalServerError, detail: exception?.Message)
        };
    }

    private ActionResult HandleRpcExceptions(RpcException exception)
    {
        return exception.StatusCode switch
        {
            Grpc.Core.StatusCode.InvalidArgument => ValidationProblem(CreateModelStateDictionary(exception)),
            Grpc.Core.StatusCode.NotFound => Problem(statusCode: (int)HttpStatusCode.NotFound,
                detail: exception.Status.Detail),
            Grpc.Core.StatusCode.PermissionDenied => Problem(statusCode: (int)HttpStatusCode.Forbidden,
                detail: exception.Status.Detail),
            Grpc.Core.StatusCode.Unauthenticated => Problem(statusCode: (int)HttpStatusCode.Unauthorized,
                detail: exception.Status.Detail),
            _ => Problem(statusCode: (int)HttpStatusCode.InternalServerError, detail: exception.Status.Detail)
        };
    }

    private static ModelStateDictionary CreateModelStateDictionary(RpcException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();

        var validationErrors =
            JsonSerializer.Deserialize<List<FluentValidationError>>(validationException.Status.Detail);

        foreach (var validationError in validationErrors)
        foreach (var error in validationError.Errors)
            modelStateDictionary.AddModelError(validationError.PropertyName, error);

        return modelStateDictionary;
    }

    /*private static ModelStateDictionary CreateModelStateDictionary(ValidationException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var failure in validationException.Errors)
            modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);

        return modelStateDictionary;
    }*/
}

file record FluentValidationError(string PropertyName, List<string> Errors);