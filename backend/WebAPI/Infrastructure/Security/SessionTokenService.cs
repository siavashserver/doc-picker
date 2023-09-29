using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Core.Common.Configurations;
using WebAPI.Core.Common.Entities;
using WebAPI.Core.Common.Interfaces;

namespace WebAPI.Infrastructure.Security;

public class SessionTokenService(IOptionsMonitor<ApplicationSecrets> applicationSecretsMonitor,
        IOptionsMonitor<ApplicationSettings> applicationSettingsMonitor)
    : ISessionTokenService
{
    private string SessionTokenSecret => applicationSecretsMonitor.CurrentValue.SessionTokenSecret;
    private int SessionTokenTimeToLive => applicationSettingsMonitor.CurrentValue.SessionTokenSettings.TimeToLive;

    public string CreateToken(Account account)
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
            Expires = DateTime.Now.AddDays(SessionTokenTimeToLive),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}