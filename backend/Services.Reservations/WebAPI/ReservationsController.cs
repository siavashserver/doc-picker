using Grpc.Core;
using Services.Reservations.Core.RequestHandlers;

namespace Services.Reservations.WebAPI;

public class ReservationsController(
    CreateReservationHandler createReservationHandler,
    GetReservationsHandler getReservationsHandler,
    UpdateReservationHandler updateReservationHandler,
    DeleteReservationHandler deleteReservationHandler
) : Reservations.ReservationsBase
{
    public override async Task<CreateReservationResponse> CreateReservation(CreateReservationRequest request,
        ServerCallContext context)
    {
        return await createReservationHandler.Handle(request);
    }

    public override async Task<GetReservationsResponse> GetReservations(GetReservationsRequest request,
        ServerCallContext context)
    {
        return await getReservationsHandler.Handle(request);
    }

    public override async Task<UpdateReservationResponse> UpdateReservation(UpdateReservationRequest request,
        ServerCallContext context)
    {
        return await updateReservationHandler.Handle(request);
    }

    public override async Task<DeleteReservationResponse> DeleteReservation(DeleteReservationRequest request,
        ServerCallContext context)
    {
        return await deleteReservationHandler.Handle(request);
    }
}