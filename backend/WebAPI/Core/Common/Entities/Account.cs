using WebAPI.Core.Common.Enums;

namespace WebAPI.Core.Common.Entities;

public class Account
{
    public int AccountId { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public AccountRole AccountRole { get; set; }
}