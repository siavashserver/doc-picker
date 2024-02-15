using MassTransit;
using Services.Accounts.Core.DataAccess;
using Services.Accounts.Shared.Events;
using Services.Shared.Core.Exceptions;
using Services.Shared.Core.Interfaces;

namespace Services.Accounts.Core.RequestHandlers;

public class DeleteAccountHandler(
    DataContext dataContext,
    IPublishEndpoint publishEndpoint
) : IRequestHandler<DeleteAccountRequest, DeleteAccountResponse>
{
    public async Task<DeleteAccountResponse> Handle(DeleteAccountRequest request)
    {
        var account = await dataContext.Accounts
            .FindAsync(request.AccountId);

        if (account is null) throw new NotFoundException();

        dataContext.Accounts.Remove(account);
        await dataContext.SaveChangesAsync();

        await publishEndpoint.Publish<AccountDeletedEvent>(new { request.AccountId });

        return new DeleteAccountResponse();
    }
}