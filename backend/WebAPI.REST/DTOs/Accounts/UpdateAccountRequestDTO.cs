using Services.Accounts;

namespace WebAPI.REST.DTOs.Accounts;

public record UpdateAccountRequestDTO(string Email, string OldPassword, string NewPassword)
{
    public static implicit operator UpdateAccountRequest(UpdateAccountRequestDTO source)
    {
        return new UpdateAccountRequest
        {
            AccountId = default,
            Email = source.Email,
            OldPassword = source.OldPassword,
            NewPassword = source.NewPassword
        };
    }
}