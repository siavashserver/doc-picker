namespace WebAPI.Core.Common.Interfaces;

public interface IPasswordService
{
    string GeneratePassword();
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}