using Grpc.Core;
using Services.Accounts.Core.RequestHandlers;

namespace Services.Accounts.WebAPI;

internal class AccountsController(
    CreateAccountHandler createAccountHandler,
    DeleteAccountHandler deleteAccountHandler,
    UpdateAccountHandler updateAccountHandler,
    GetAccountHandler getAccountHandler,
    LoginAccountHandler loginAccountHandler
) : Accounts.AccountsBase
{
    public override async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request,
        ServerCallContext context)
    {
        return await createAccountHandler.Handle(request);
    }

    public override async Task<GetAccountResponse> GetAccount(GetAccountRequest request, ServerCallContext context)
    {
        return await getAccountHandler.Handle(request);
    }

    public override async Task<UpdateAccountResponse> UpdateAccount(UpdateAccountRequest request,
        ServerCallContext context)
    {
        return await updateAccountHandler.Handle(request);
    }

    public override async Task<DeleteAccountResponse> DeleteAccount(DeleteAccountRequest request,
        ServerCallContext context)
    {
        return await deleteAccountHandler.Handle(request);
    }

    public override async Task<LoginAccountResponse> LoginAccount(LoginAccountRequest request,
        ServerCallContext context)
    {
        return await loginAccountHandler.Handle(request);
    }
}