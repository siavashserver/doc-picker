using FluentValidation;

namespace Services.Accounts.Core.RequestHandlers;

public class GetAccountValidator : AbstractValidator<GetAccountRequest>
{
    public GetAccountValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
    }
}