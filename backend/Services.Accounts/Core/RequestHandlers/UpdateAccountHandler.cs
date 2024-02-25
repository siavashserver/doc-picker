using Services.Accounts.Core.DataAccess;
using Services.Accounts.Core.Exceptions;
using Services.Accounts.Core.Interfaces;
using Services.Shared.Core.Exceptions;
using Services.Shared.Core.Interfaces;

namespace Services.Accounts.Core.RequestHandlers;

public class UpdateAccountHandler(
    DataContext dataContext,
    IPasswordService passwordService
) : IRequestHandler<UpdateAccountRequest, UpdateAccountResponse>
{
    public async Task<UpdateAccountResponse> Handle(UpdateAccountRequest request)
    {
        var account = await dataContext.Accounts
            .FindAsync(request.AccountId);

        if (account is null) throw new NotFoundException();

        account.Email = request.Email ?? account.Email;

        if (request.OldPassword is not null && request.NewPassword is not null)
        {
            if (false == passwordService.VerifyPassword(request.OldPassword, account.PasswordHash))
                throw new IncorrectPasswordException();

            account.PasswordHash = passwordService.HashPassword(request.NewPassword);
        }

        await dataContext.SaveChangesAsync();

        return new UpdateAccountResponse();
    }
}