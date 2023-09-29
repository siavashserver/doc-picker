using System.Security.Claims;
using WebAPI.Core.Common.Interfaces;

namespace WebAPI.Infrastructure.Security;

public class CurrentAccountService(IHttpContextAccessor httpContextAccessor) : ICurrentAccountService
{
    public int AccountId =>
        Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public string Email => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
}