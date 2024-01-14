using Services.Accounts.Core.DataAccess;
using Services.Shared.Core.Exceptions;
using Services.Shared.Core.Interfaces;

namespace Services.Accounts.Core.RequestHandlers;

public class GetAccountHandler(
    DataContext dataContext
) : IRequestHandler<GetAccountRequest, GetAccountResponse>
{
    public async Task<GetAccountResponse> Handle(GetAccountRequest request)
    {
        var account = await dataContext.Accounts
            .FindAsync(request.AccountId);

        if (account is null) throw new NotFoundException();

        return new GetAccountResponse
        {
            AccountId = account.AccountId,
            Email = account.Email
        };
    }
}