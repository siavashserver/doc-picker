using Services.Accounts;

namespace WebAPI.REST.DTOs.Accounts;

public record CreateAccountRequestDTO(string Email, string Password)
{
    public static implicit operator CreateAccountRequest(CreateAccountRequestDTO source)
    {
        return new CreateAccountRequest
        {
            Email = source.Email,
            Password = source.Password
        };
    }
}