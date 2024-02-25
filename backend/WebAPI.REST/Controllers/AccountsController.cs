using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using Services.Accounts;
using WebAPI.REST.DTOs.Accounts;

namespace WebAPI.REST.Controllers;

public class AccountsController(GrpcClientFactory grpcClientFactory) : BaseAPIController
{
    private readonly Accounts.AccountsClient _accountsClient =
        grpcClientFactory.CreateClient<Accounts.AccountsClient>(nameof(Accounts.AccountsClient));

    [HttpPost]
    public async Task<ActionResult<CreateAccountResponseDTO>> CreateAccount(CreateAccountRequestDTO request)
    {
        var response = await _accountsClient.CreateAccountAsync(request);
        return CreatedAtRoute(
            nameof(GetAccount),
            new { id = response.AccountId },
            response);
    }

    [HttpGet("{id:int}", Name = nameof(GetAccount))]
    public async Task<ActionResult<GetAccountResponseDTO>> GetAccount(int id)
    {
        var response = await _accountsClient.GetAccountAsync(new GetAccountRequest { AccountId = id });
        return Ok(response);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateAccount(int id, UpdateAccountRequestDTO request)
    {
        UpdateAccountRequest _request = request;
        _request.AccountId = id;
        var response = await _accountsClient.UpdateAccountAsync(_request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAccount(int id)
    {
        var response = await _accountsClient.DeleteAccountAsync(new DeleteAccountRequest { AccountId = id });
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginAccountResponseDTO>> LoginAccount(LoginAccountRequestDTO request)
    {
        var response = await _accountsClient.LoginAccountAsync(request);
        return Ok(response);
    }
}