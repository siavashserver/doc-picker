namespace WebAPI.REST.DTOs.Doctors;

public record GetDoctorsResultDTO(float? Score, string DoctorId, string Name, string SpecialityId)
{
}