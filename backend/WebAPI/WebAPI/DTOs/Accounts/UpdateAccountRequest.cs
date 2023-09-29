namespace WebAPI.WebAPI.DTOs.Accounts;

public class UpdateAccountRequest
{
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}