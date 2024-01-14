using FluentValidation;
using Microsoft.Extensions.Options;
using Services.Accounts.Core.Settings;

namespace Services.Accounts.Core.RequestHandlers;

public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
{
    private readonly IOptionsMonitor<ApplicationSettings> _applicationSettings;

    public CreateAccountValidator(IOptionsMonitor<ApplicationSettings> applicationSettings)
    {
        _applicationSettings = applicationSettings;

        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(MinimumPasswordLength);
    }

    private int MinimumPasswordLength => _applicationSettings.CurrentValue.PasswordSettings.MinimumPasswordLength;
}