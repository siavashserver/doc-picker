using Services.Reservations;

namespace WebAPI.REST.DTOs.Reservations;

public record CreateReservationResponseDTO(int ReservationId)
{
    public static implicit operator CreateReservationResponseDTO(CreateReservationResponse source)
    {
        return new CreateReservationResponseDTO(source.ReservationId);
    }
}