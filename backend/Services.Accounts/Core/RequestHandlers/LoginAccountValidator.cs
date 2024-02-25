using FluentValidation;

namespace Services.Accounts.Core.RequestHandlers;

public class LoginAccountValidator : AbstractValidator<LoginAccountRequest>
{
    public LoginAccountValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}