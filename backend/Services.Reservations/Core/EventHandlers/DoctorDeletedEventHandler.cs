using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Doctors.Shared.Events;
using Services.Reservations.Core.DataAccess;

namespace Services.Reservations.Core.EventHandlers;

public class DoctorDeletedEventHandler(DataContext dataContext) : IConsumer<DoctorDeletedEvent>
{
    public async Task Consume(ConsumeContext<DoctorDeletedEvent> context)
    {
        await dataContext.Reservations.Where(reservation => reservation.DoctorId == context.Message.DoctorId)
            .ExecuteDeleteAsync();
    }
}