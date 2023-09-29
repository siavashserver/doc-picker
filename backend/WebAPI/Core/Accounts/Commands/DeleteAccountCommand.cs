using FluentValidation;
using MediatR;
using WebAPI.Core.Common.Exceptions;
using WebAPI.Infrastructure.DataAccess;

namespace WebAPI.Core.Accounts.Commands;

public static class DeleteAccountCommand
{
    public record Request(int AccountId) : IRequest;

    public class Handler(DataContext dataContext) : IRequestHandler<Request>
    {
        public async Task Handle(Request request, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts
                .FindAsync(
                    new object?[] { request.AccountId },
                    cancellationToken);

            if (account is null) throw new NotFoundException();

            dataContext.Accounts.Remove(account);
            await dataContext.SaveChangesAsync(cancellationToken);
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