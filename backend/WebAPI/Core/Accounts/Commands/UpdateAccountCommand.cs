using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using WebAPI.Core.Common.Configurations;
using WebAPI.Core.Common.Exceptions;
using WebAPI.Core.Common.Interfaces;
using WebAPI.Infrastructure.DataAccess;

namespace WebAPI.Core.Accounts.Commands;

public static class UpdateAccountCommand
{
    public record Request(int AccountId, string? Email, string? OldPassword, string? NewPassword) : IRequest;

    public class Handler(DataContext dataContext, IPasswordService passwordService) : IRequestHandler<Request>
    {
        public async Task Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts
                .FindAsync(
                    new object?[] { request.AccountId },
                    cancellationToken);

            if (account is null) throw new NotFoundException();

            account.Email = request.Email ?? account.Email;

            if (request.OldPassword is not null && request.NewPassword is not null)
            {
                if (false == passwordService.VerifyPassword(request.OldPassword, account.PasswordHash))
                    throw new IncorrectPasswordException();

                account.PasswordHash = passwordService.HashPassword(request.NewPassword);
            }

            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        private readonly IOptionsMonitor<ApplicationSettings> _applicationSettingsMonitor;

        public Validator(IOptionsMonitor<ApplicationSettings> applicationSettingsMonitor)
        {
            _applicationSettingsMonitor = applicationSettingsMonitor;

            RuleFor(request => request.AccountId)
                .NotEmpty();

            When(request => request.Email is not null, () =>
            {
                RuleFor(request => request.Email)
                    .NotEmpty()
                    .EmailAddress();
            });

            When(request => request.OldPassword is not null || request.NewPassword is not null, () =>
            {
                RuleFor(request => request.OldPassword)
                    .NotEmpty();

                // TODO: Reuse password validation logic
                RuleFor(request => request.NewPassword)
                    .NotEmpty()
                    .NotEqual(request => request.OldPassword)
                    .WithMessage("New password should not be same as the old password.")
                    .MinimumLength(MinimumPasswordLength);
            });
        }

        private int MinimumPasswordLength =>
            _applicationSettingsMonitor.CurrentValue.PasswordSettings.MinimumPasswordLength;
    }
}