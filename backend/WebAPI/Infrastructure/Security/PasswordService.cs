using Microsoft.Extensions.Options;
using PasswordGenerator;
using WebAPI.Core.Common.Configurations;
using WebAPI.Core.Common.Interfaces;

namespace WebAPI.Infrastructure.Security;

public class PasswordService : IPasswordService
{
    private readonly IOptionsMonitor<ApplicationSettings> _applicationSettingsMonitor;
    private readonly IPassword _password;

    public PasswordService(IOptionsMonitor<ApplicationSettings> applicationSettingsMonitor)
    {
        _applicationSettingsMonitor = applicationSettingsMonitor;

        _password = new Password()
            .LengthRequired(MinimumPasswordLength)
            .IncludeNumeric()
            .IncludeLowercase()
            .IncludeUppercase();
    }

    private int MinimumPasswordLength =>
        _applicationSettingsMonitor.CurrentValue.PasswordSettings.MinimumPasswordLength;

    public string GeneratePassword()
    {
        var password = _password.Next();
        return password;
    }

    public string HashPassword(string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        return passwordHash;
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var matches = BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        return matches;
    }
}