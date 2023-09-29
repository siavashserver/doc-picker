using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAPI.Core.Common.Exceptions;

namespace WebAPI.WebAPI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : BaseAPIController
{
    [AllowAnonymous]
    public ActionResult ErrorHandler()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return exception switch
        {
            ValidationException exc => ValidationProblem(CreateModelStateDictionary(exc)),
            BaseException exc => Problem(statusCode: (int)exc.GetHttpStatusCode(), detail: exc.Message),
            _ => Problem(statusCode: (int)HttpStatusCode.InternalServerError, detail: exception?.Message)
        };
    }

    private static ModelStateDictionary CreateModelStateDictionary(ValidationException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var failure in validationException.Errors)
            modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);

        return modelStateDictionary;
    }
}