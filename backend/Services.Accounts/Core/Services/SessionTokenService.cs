using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Accounts.Core.DataAccess.Entities;
using Services.Accounts.Core.Interfaces;
using Services.Accounts.Core.Secrets;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Services.Accounts.Core.Services;

public class SessionTokenService(
    IOptionsMonitor<ApplicationSecrets> applicationSecretsMonitor
) : ISessionTokenService
{
    private string SessionTokenSecret => applicationSecretsMonitor.CurrentValue.SessionTokenSecret;

    public string CreateToken(Account account, int tokenTimeToLive)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, account.AccountId.ToString()),
            new(JwtRegisteredClaimNames.Email, account.Email),
            new(ClaimTypes.Role, account.AccountRole.ToString())
        };

        var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SessionTokenSecret));
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(tokenTimeToLive),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}