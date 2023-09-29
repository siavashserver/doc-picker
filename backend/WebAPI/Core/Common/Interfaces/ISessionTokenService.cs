using WebAPI.Core.Common.Entities;

namespace WebAPI.Core.Common.Interfaces;

public interface ISessionTokenService
{
    string CreateToken(Account account);
}