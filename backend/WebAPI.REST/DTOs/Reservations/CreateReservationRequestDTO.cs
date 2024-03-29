using Google.Protobuf.WellKnownTypes;
using Services.Reservations;

namespace WebAPI.REST.DTOs.Reservations;

public record CreateReservationRequestDTO(int PatientId, string DoctorId, DateOnly Date, int Shift)
{
    public static implicit operator CreateReservationRequest(CreateReservationRequestDTO source)
    {
        return new CreateReservationRequest
        {
            PatientId = source.PatientId,
            DoctorId = source.DoctorId,
            Date = source.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc).ToTimestamp(),
            Shift = source.Shift
        };
    }
}