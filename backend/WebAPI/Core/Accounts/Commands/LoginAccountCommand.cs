using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Common.Exceptions;
using WebAPI.Core.Common.Interfaces;
using WebAPI.Infrastructure.DataAccess;

namespace WebAPI.Core.Accounts.Commands;

public static class LoginAccountCommand
{
    public record Request(string Email, string Password) : IRequest<Response>;

    public record Response(string Token);

    public class Handler(DataContext dataContext,
            IPasswordService passwordService,
            ISessionTokenService sessionTokenService)
        : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await dataContext
                .Accounts
                .SingleOrDefaultAsync(
                    accounts => accounts.Email == request.Email,
                    cancellationToken);

            if (account is null) throw new IncorrectCredentialsException();

            var passwordMatches = passwordService.VerifyPassword(request.Password, account.PasswordHash);
            if (false == passwordMatches) throw new IncorrectCredentialsException();

            var token = sessionTokenService.CreateToken(account);
            return new Response(token);
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress();

            // TODO: Reuse password validation logic
            RuleFor(request => request.Password)
                .NotEmpty();
        }
    }
}