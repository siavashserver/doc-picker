using Services.Reservations;

namespace WebAPI.REST.DTOs.Reservations;

public record GetReservationsResponseDTO(bool HasNextPage, List<GetReservationsResultDTO> Reservations)
{
    public static implicit operator GetReservationsResponseDTO(GetReservationsResponse source)
    {
        return new GetReservationsResponseDTO(
            source.HasNextPage,
            source.Reservations.Select(reservation => new GetReservationsResultDTO(reservation.ReservationId,
                reservation.PatientId, reservation.DoctorId, DateOnly.FromDateTime(reservation.Date.ToDateTime()),
                reservation.Shift)).ToList()
        );
    }
}