using Services.Accounts.Core.DataAccess;
using Services.Accounts.Core.DataAccess.Entities;
using Services.Accounts.Core.Interfaces;
using Services.Shared.Core.Interfaces;

namespace Services.Accounts.Core.RequestHandlers;

public class CreateAccountHandler(
    DataContext dataContext,
    IPasswordService passwordService
) : IRequestHandler<CreateAccountRequest, CreateAccountResponse>
{
    public async Task<CreateAccountResponse> Handle(CreateAccountRequest request)
    {
        var passwordHash = passwordService.HashPassword(request.Password);

        var account = new Account
        {
            Email = request.Email,
            PasswordHash = passwordHash
        };
        await dataContext.Accounts.AddAsync(account);

        await dataContext.SaveChangesAsync();

        return new CreateAccountResponse
        {
            AccountId = account.AccountId
        };
    }
}