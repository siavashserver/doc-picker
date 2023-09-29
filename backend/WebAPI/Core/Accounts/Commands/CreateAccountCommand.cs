using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using WebAPI.Core.Common.Configurations;
using WebAPI.Core.Common.Entities;
using WebAPI.Core.Common.Interfaces;
using WebAPI.Infrastructure.DataAccess;

namespace WebAPI.Core.Accounts.Commands;

public static class CreateAccountCommand
{
    public record Request(string Email, string Password) : IRequest<Response>;

    public record Response(int AccountId);

    public class Handler(DataContext dataContext, IPasswordService passwordService) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var passwordHash = passwordService.HashPassword(request.Password);

            var account = new Account
            {
                Email = request.Email,
                PasswordHash = passwordHash
            };
            dataContext.Accounts.Add(account);

            await dataContext.SaveChangesAsync(cancellationToken);

            return new Response(account.AccountId);
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        private readonly IOptionsMonitor<ApplicationSettings> _applicationSettingsMonitor;

        public Validator(IOptionsMonitor<ApplicationSettings> applicationSettingsMonitor)
        {
            _applicationSettingsMonitor = applicationSettingsMonitor;

            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress();

            // TODO: Reuse password validation logic
            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(MinimumPasswordLength);
        }

        private int MinimumPasswordLength =>
            _applicationSettingsMonitor.CurrentValue.PasswordSettings.MinimumPasswordLength;
    }
}