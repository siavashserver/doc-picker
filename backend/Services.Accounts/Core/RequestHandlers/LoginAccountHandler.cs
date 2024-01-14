using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services.Accounts.Core.DataAccess;
using Services.Accounts.Core.Exceptions;
using Services.Accounts.Core.Interfaces;
using Services.Accounts.Core.Settings;
using Services.Shared.Core.Interfaces;

namespace Services.Accounts.Core.RequestHandlers;

public class LoginAccountHandler(
    DataContext dataContext,
    IPasswordService passwordService,
    ISessionTokenService sessionTokenService,
    IOptionsMonitor<ApplicationSettings> applicationSettingsMonitor
) : IRequestHandler<LoginAccountRequest, LoginAccountResponse>
{
    private int AccessTokenTimeToLive => applicationSettingsMonitor.CurrentValue.SessionTokenSettings.AccessTokenTTL;

    private int RefreshTokenTimeToLive => applicationSettingsMonitor.CurrentValue.SessionTokenSettings.RefreshTokenTTL;

    public async Task<LoginAccountResponse> Handle(LoginAccountRequest request)
    {
        var account = await dataContext
            .Accounts
            .SingleOrDefaultAsync(
                accounts => accounts.Email == request.Email
            );

        if (account is null) throw new IncorrectCredentialsException();

        var passwordMatches = passwordService.VerifyPassword(request.Password, account.PasswordHash);
        if (false == passwordMatches) throw new IncorrectCredentialsException();

        var accessToken = sessionTokenService.CreateToken(account, AccessTokenTimeToLive);
        var refreshToken = sessionTokenService.CreateToken(account, RefreshTokenTimeToLive);

        return new LoginAccountResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}