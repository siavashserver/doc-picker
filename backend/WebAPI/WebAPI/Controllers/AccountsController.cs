using Microsoft.AspNetCore.Mvc;
using WebAPI.Core.Accounts.Commands;
using WebAPI.Core.Accounts.Queries;
using WebAPI.WebAPI.DTOs.Accounts;

namespace WebAPI.WebAPI.Controllers;

public class AccountsController : BaseAPIController
{
    [HttpPost]
    public async Task<ActionResult<CreateAccountResponse>> CreateAccount(CreateAccountRequest request)
    {
        var command = Mapper.Map<CreateAccountCommand.Request>(request);
        var result = await Mediator.Send(command);
        var response = Mapper.Map<CreateAccountResponse>(result);
        return CreatedAtRoute(
            nameof(GetAccount),
            new { id = response.AccountId },
            response);
    }

    [HttpGet("{id:int}", Name = nameof(GetAccount))]
    public async Task<ActionResult<GetAccountResponse>> GetAccount(int id)
    {
        var command = new GetAccountQuery.Request(id);
        var result = await Mediator.Send(command);
        var response = Mapper.Map<GetAccountResponse>(result);
        return Ok(response);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateAccount(int id, UpdateAccountRequest request)
    {
        var command = Mapper.Map<UpdateAccountCommand.Request>(request);
        command = command with { AccountId = id };
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAccount(int id)
    {
        var command = new DeleteAccountCommand.Request(id);
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginAccountResponse>> Login(LoginAccountRequest request)
    {
        var command = Mapper.Map<LoginAccountCommand.Request>(request);
        var result = await Mediator.Send(command);
        var response = Mapper.Map<LoginAccountResponse>(result);
        return response;
    }
}