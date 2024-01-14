using Services.Reservations;

namespace WebAPI.REST.DTOs.Reservations;

public record DeleteReservationRequestDTO(int ReservationId)
{
    public static implicit operator DeleteReservationRequest(DeleteReservationRequestDTO source)
    {
        return new DeleteReservationRequest { ReservationId = source.ReservationId };
    }
}