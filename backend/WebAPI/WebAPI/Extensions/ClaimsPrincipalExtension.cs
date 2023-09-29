using System.Security.Claims;
using WebAPI.Core.Common.Enums;

namespace WebAPI.WebAPI.Extensions;

public static class ClaimsPrincipalExtension
{
    public static int? GetAccountId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return claim is null ? null : Convert.ToInt32(claim);
    }

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        var claim = user.FindFirstValue(ClaimTypes.Email);
        return claim ?? null;
    }

    public static AccountRole? GetRole(this ClaimsPrincipal user)
    {
        var claim = user.FindFirstValue(ClaimTypes.Role);
        return claim is null ? null : Enum.Parse<AccountRole>(claim);
    }
}