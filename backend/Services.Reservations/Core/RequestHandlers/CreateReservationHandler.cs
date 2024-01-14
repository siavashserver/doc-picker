using Services.Reservations.Core.DataAccess;
using Services.Reservations.Core.DataAccess.Entities;
using Services.Shared.Core.Interfaces;

namespace Services.Reservations.Core.RequestHandlers;

public class CreateReservationHandler(
    DataContext dataContext
) : IRequestHandler<CreateReservationRequest, CreateReservationResponse>
{
    public async Task<CreateReservationResponse> Handle(CreateReservationRequest request)
    {
        var reservation = new Reservation
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            Date = DateOnly.FromDateTime(request.Date.ToDateTime()),
            Shift = request.Shift
        };
        await dataContext.Reservations.AddAsync(reservation);

        await dataContext.SaveChangesAsync();

        return new CreateReservationResponse
        {
            ReservationId = reservation.ReservationId
        };
    }
}