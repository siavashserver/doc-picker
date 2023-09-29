using AutoMapper;
using WebAPI.Core.Accounts.Commands;
using WebAPI.Core.Accounts.Queries;
using WebAPI.WebAPI.DTOs.Accounts;

namespace WebAPI.WebAPI.DTOs;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateAccountsProfiles();
    }

    private void CreateAccountsProfiles()
    {
        CreateMap<CreateAccountRequest, CreateAccountCommand.Request>();
        CreateMap<CreateAccountCommand.Response, CreateAccountResponse>();

        CreateMap<GetAccountQuery.Response, GetAccountResponse>();

        CreateMap<UpdateAccountRequest, UpdateAccountCommand.Request>()
            .ForCtorParam(nameof(UpdateAccountCommand.Request.AccountId), cfg => cfg.MapFrom(src => 0));

        CreateMap<LoginAccountRequest, LoginAccountCommand.Request>();
        CreateMap<LoginAccountCommand.Response, LoginAccountResponse>();
    }
}