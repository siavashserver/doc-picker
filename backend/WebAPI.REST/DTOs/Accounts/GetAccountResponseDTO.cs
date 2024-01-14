using Services.Accounts;

namespace WebAPI.REST.DTOs.Accounts;

public record GetAccountResponseDTO(int AccountId, string Email)
{
    public static implicit operator GetAccountResponseDTO(GetAccountResponse source)
    {
        return new GetAccountResponseDTO(source.AccountId, source.Email);
    }
}