using Microsoft.AspNetCore.Identity;
using Services.Accounts.Core.DataAccess.Entities;
using Services.Accounts.Core.Interfaces;

namespace Services.Accounts.Core.Services;

public class PasswordService(
    IPasswordHasher<Account> passwordHasher
) : IPasswordService
{
    public string HashPassword(string password)
    {
        var passwordHash = passwordHasher.HashPassword(default, password);
        return passwordHash;
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var matches = passwordHasher.VerifyHashedPassword(default, passwordHash, password) ==
                      PasswordVerificationResult.Success;
        return matches;
    }
}