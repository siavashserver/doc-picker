using Services.Doctors;

namespace WebAPI.REST.DTOs.Doctors;

public record UpdateDoctorRequestDTO(string Name, string SpecialityId)
{
    public static implicit operator UpdateDoctorRequest(UpdateDoctorRequestDTO source)
    {
        return new UpdateDoctorRequest
        {
            DoctorId = default,
            Name = source.Name,
            SpecialityId = source.SpecialityId
        };
    }
}