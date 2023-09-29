using FluentValidation;
using MediatR;
using WebAPI.Core.Common.Exceptions;
using WebAPI.Infrastructure.DataAccess;

namespace WebAPI.Core.Accounts.Queries;

public static class GetAccountQuery
{
    public record Request(int AccountId) : IRequest<Response>;

    public record Response(int AccountId, string Email);

    public class Handler(DataContext dataContext) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts
                .FindAsync(
                    new object?[] { request.AccountId },
                    cancellationToken);

            if (account is null) throw new NotFoundException();

            return new Response(account.AccountId, account.Email);
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.AccountId)
                .NotEmpty();
        }
    }
}