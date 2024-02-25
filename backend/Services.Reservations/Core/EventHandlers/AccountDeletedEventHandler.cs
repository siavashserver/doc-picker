using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Accounts.Shared.Events;
using Services.Reservations.Core.DataAccess;

namespace Services.Reservations.Core.EventHandlers;

public class AccountDeletedEventHandler(DataContext dataContext) : IConsumer<AccountDeletedEvent>
{
    public async Task Consume(ConsumeContext<AccountDeletedEvent> context)
    {
        await dataContext.Reservations.Where(reservation => reservation.PatientId == context.Message.AccountId)
            .ExecuteDeleteAsync();
    }
}