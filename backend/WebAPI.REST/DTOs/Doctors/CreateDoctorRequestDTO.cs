using Services.Doctors;

namespace WebAPI.REST.DTOs.Doctors;

public record CreateDoctorRequestDTO(string Name, string SpecialityId)
{
    public static implicit operator CreateDoctorRequest(CreateDoctorRequestDTO source)
    {
        return new CreateDoctorRequest
        {
            Name = source.Name,
            SpecialityId = source.SpecialityId
        };
    }
}