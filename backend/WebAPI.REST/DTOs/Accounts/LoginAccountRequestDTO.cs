using Services.Accounts;

namespace WebAPI.REST.DTOs.Accounts;

public record LoginAccountRequestDTO(string Email, string Password)
{
    public static implicit operator LoginAccountRequest(LoginAccountRequestDTO source)
    {
        return new LoginAccountRequest
        {
            Email = source.Email,
            Password = source.Password
        };
    }
}