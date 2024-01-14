using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Services.Reservations.Core.DataAccess;
using Services.Shared.Core.Interfaces;
using Services.Shared.Extensions;

namespace Services.Reservations.Core.RequestHandlers;

public class GetReservationsHandler(
    DataContext dataContext
) : IRequestHandler<GetReservationsRequest, GetReservationsResponse>
{
    public async Task<GetReservationsResponse> Handle(GetReservationsRequest request)
    {
        if (0 < request.ReservationIds.Count) return await HandleReservationIdsField(request);

        return await HandleOtherFields(request);
    }

    private async Task<GetReservationsResponse> HandleReservationIdsField(GetReservationsRequest request)
    {
        var reservations = await dataContext.Reservations
            .Where(reservation => request.ReservationIds.Contains(reservation.ReservationId))
            .Select(reservation => new GetReservationsResult
            {
                ReservationId = reservation.ReservationId,
                PatientId = reservation.PatientId,
                DoctorId = reservation.DoctorId,
                Date = reservation.Date.ToDateTime(TimeOnly.MinValue).ToTimestamp(),
                Shift = reservation.Shift
            })
            .ToListAsync();

        return new GetReservationsResponse
        {
            HasNextPage = false,
            Reservations = { reservations }
        };
    }

    private async Task<GetReservationsResponse> HandleOtherFields(GetReservationsRequest request)
    {
        var query = dataContext.Reservations.AsQueryable();

        if (0 < request.PatientIds.Count)
            query = query.Where(reservation => request.PatientIds.Contains(reservation.PatientId));

        if (0 < request.DoctorIds.Count)
            query = query.Where(reservation => request.DoctorIds.Contains(reservation.DoctorId));

        query = query.Where(reservation =>
            reservation.Date >= DateOnly.FromDateTime(request.Dates.Start.ToDateTime()) &&
            reservation.Date <= DateOnly.FromDateTime(request.Dates.End.ToDateTime()));

        query = query.Where(r => r.Shift >= request.Shifts.Start && r.Shift <= request.Shifts.End);

        var totalReservations = await query.CountAsync();
        var hasNextPage = request.Page * request.PageSize < totalReservations;

        if (1 > totalReservations)
            return new GetReservationsResponse
            {
                HasNextPage = hasNextPage
            };

        var reservations = await query.Paginate(request.Page, request.PageSize)
            .Select(reservation => new GetReservationsResult
            {
                ReservationId = reservation.ReservationId,
                PatientId = reservation.PatientId,
                DoctorId = reservation.DoctorId,
                Date = reservation.Date.ToDateTime(TimeOnly.MinValue).ToTimestamp(),
                Shift = reservation.Shift
            })
            .ToListAsync();

        return new GetReservationsResponse
        {
            HasNextPage = hasNextPage,
            Reservations = { reservations }
        };
    }
}