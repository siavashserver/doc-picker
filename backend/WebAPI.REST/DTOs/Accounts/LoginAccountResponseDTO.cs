using Services.Accounts;

namespace WebAPI.REST.DTOs.Accounts;

public record LoginAccountResponseDTO(string AccessToken, string RefreshToken)
{
    public static implicit operator LoginAccountResponseDTO(LoginAccountResponse source)
    {
        return new LoginAccountResponseDTO(source.AccessToken, source.RefreshToken);
    }
}