using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Core.Common.Enums;
using WebAPI.WebAPI.Extensions;

namespace WebAPI.WebAPI.Filters;

public class AuthorizeAttribute(AccountRole requiredAccountRole) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var accountRole = context.HttpContext.User.GetRole();
        if (accountRole is null || accountRole < requiredAccountRole)
            context.Result = new JsonResult(new { message = "Unauthorized" })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
    }
}