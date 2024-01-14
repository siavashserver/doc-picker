using Google.Protobuf.WellKnownTypes;
using Services.Reservations;

namespace WebAPI.REST.DTOs.Reservations;

public record UpdateReservationRequestDTO(int ReservationId, int PatientId, string DoctorId, DateOnly Date, int Shift)
{
    public static implicit operator UpdateReservationRequest(UpdateReservationRequestDTO source)
    {
        return new UpdateReservationRequest
        {
            ReservationId = source.ReservationId,
            PatientId = source.PatientId,
            DoctorId = source.DoctorId,
            Date = source.Date.ToDateTime(TimeOnly.MinValue).ToTimestamp(),
            Shift = source.Shift
        };
    }
}