using Services.Doctors;

namespace WebAPI.REST.DTOs.Doctors;

public record CreateDoctorResponseDTO(string DoctorId)
{
    public static implicit operator CreateDoctorResponseDTO(CreateDoctorResponse source)
    {
        return new CreateDoctorResponseDTO(source.DoctorId);
    }
}