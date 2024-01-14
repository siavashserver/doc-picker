using Services.Reservations;

namespace WebAPI.REST.DTOs.Reservations;

public record GetReservationResponseDTO(int ReservationId, int PatientId, string DoctorId, DateOnly Date, int Shift)
{
    public static implicit operator GetReservationResponseDTO(GetReservationsResponse source)
    {
        var reservation = source.Reservations[0];
        return new GetReservationResponseDTO(reservation.ReservationId, reservation.PatientId, reservation.DoctorId,
            DateOnly.FromDateTime(reservation.Date.ToDateTime()), reservation.Shift);
    }
}