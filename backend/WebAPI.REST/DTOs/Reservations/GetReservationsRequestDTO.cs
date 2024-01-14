using Google.Protobuf.WellKnownTypes;
using Services.Reservations;

namespace WebAPI.REST.DTOs.Reservations;

public record GetReservationsRequestDTO(
    int Page,
    int PageSize,
    List<int> ReservationIds,
    List<int> PatientIds,
    List<string> DoctorIds,
    ReservationDateRangeDTO Dates,
    ReservationShiftRangeDTO Shifts)
{
    public static implicit operator GetReservationsRequest(GetReservationsRequestDTO source)
    {
        return new GetReservationsRequest
        {
            Page = source.Page,
            PageSize = source.PageSize,
            ReservationIds = { source.ReservationIds },
            PatientIds = { source.PatientIds },
            DoctorIds = { source.DoctorIds },
            Dates = new ReservationDateRange
            {
                Start = source.Dates.Start.ToDateTime(TimeOnly.MinValue).ToTimestamp(),
                End = source.Dates.End.ToDateTime(TimeOnly.MinValue).ToTimestamp()
            },
            Shifts = new ReservationShiftRange
            {
                Start = source.Shifts.Start,
                End = source.Shifts.End
            }
        };
    }
}