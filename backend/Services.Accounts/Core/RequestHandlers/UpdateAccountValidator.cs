using FluentValidation;
using Microsoft.Extensions.Options;
using Services.Accounts.Core.Settings;

namespace Services.Accounts.Core.RequestHandlers;

public class UpdateAccountValidator : AbstractValidator<UpdateAccountRequest>
{
    private readonly IOptionsMonitor<ApplicationSettings> _applicationSettings;

    public UpdateAccountValidator(IOptionsMonitor<ApplicationSettings> applicationSettings)
    {
        _applicationSettings = applicationSettings;

        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.OldPassword).NotEmpty();
        RuleFor(x => x.NewPassword).MinimumLength(MinimumPasswordLength);
    }

    private int MinimumPasswordLength => _applicationSettings.CurrentValue.PasswordSettings.MinimumPasswordLength;
}