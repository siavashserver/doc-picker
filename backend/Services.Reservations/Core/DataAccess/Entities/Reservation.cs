namespace Services.Reservations.Core.DataAccess.Entities;

public class Reservation
{
    public int ReservationId { get; set; }
    public int PatientId { get; set; }
    public string DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public int Shift { get; set; }
}