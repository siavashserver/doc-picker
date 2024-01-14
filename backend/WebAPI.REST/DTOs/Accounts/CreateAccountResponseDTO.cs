using Services.Accounts;

namespace WebAPI.REST.DTOs.Accounts;

public record CreateAccountResponseDTO(int AccountId)
{
    public static implicit operator CreateAccountResponseDTO(CreateAccountResponse source)
    {
        return new CreateAccountResponseDTO(source.AccountId);
    }
}