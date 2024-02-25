namespace WebAPI.REST.DTOs.Reservations;

public record GetReservationsResultDTO(int ReservationId, int PatientId, string DoctorId, DateOnly Date, int Shift);