using Services.Reservations.Core.DataAccess;
using Services.Shared.Core.Exceptions;
using Services.Shared.Core.Interfaces;

namespace Services.Reservations.Core.RequestHandlers;

public class UpdateReservationHandler(
    DataContext dataContext
) : IRequestHandler<UpdateReservationRequest, UpdateReservationResponse>
{
    public async Task<UpdateReservationResponse> Handle(UpdateReservationRequest request)
    {
        var reservation = await dataContext.Reservations.FindAsync(request.ReservationId);

        if (reservation is null) throw new NotFoundException();

        reservation.PatientId = request.PatientId ?? reservation.PatientId;
        reservation.DoctorId = request.DoctorId ?? reservation.DoctorId;
        reservation.Date = request.Date != null ? DateOnly.FromDateTime(request.Date.ToDateTime()) : reservation.Date;
        reservation.Shift = request.Shift ?? reservation.Shift;

        await dataContext.SaveChangesAsync();

        return new UpdateReservationResponse();
    }
}