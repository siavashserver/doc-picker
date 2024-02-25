using Services.Accounts.Core.DataAccess.Entities;

namespace Services.Accounts.Core.Interfaces;

public interface ISessionTokenService
{
    string CreateToken(Account account, int tokenTimeToLive);
}