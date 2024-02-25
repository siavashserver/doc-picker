using Services.Reservations.Core.DataAccess;
using Services.Shared.Core.Exceptions;
using Services.Shared.Core.Interfaces;

namespace Services.Reservations.Core.RequestHandlers;

public class DeleteReservationHandler(
    DataContext dataContext
) : IRequestHandler<DeleteReservationRequest, DeleteReservationResponse>
{
    public async Task<DeleteReservationResponse> Handle(DeleteReservationRequest request)
    {
        var reservation = await dataContext.Reservations.FindAsync(request.ReservationId);

        if (reservation is null) throw new NotFoundException();

        dataContext.Reservations.Remove(reservation);
        await dataContext.SaveChangesAsync();

        return new DeleteReservationResponse();
    }
}