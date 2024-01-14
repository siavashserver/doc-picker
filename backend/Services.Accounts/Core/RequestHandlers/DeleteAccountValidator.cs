using FluentValidation;

namespace Services.Accounts.Core.RequestHandlers;

public class DeleteAccountValidator : AbstractValidator<DeleteAccountRequest>
{
    public DeleteAccountValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
    }
}