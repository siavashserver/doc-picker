namespace WebAPI.Core.Common.Interfaces;

public interface ICurrentAccountService
{
    int AccountId { get; }
    string Email { get; }
}