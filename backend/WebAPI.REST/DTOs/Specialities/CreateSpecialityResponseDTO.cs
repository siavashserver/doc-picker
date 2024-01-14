using Services.Doctors;

namespace WebAPI.REST.DTOs.Specialities;

public record CreateSpecialityResponseDTO(string SpecialityId)
{
    public static implicit operator CreateSpecialityResponseDTO(CreateSpecialityResponse source)
    {
        return new CreateSpecialityResponseDTO(source.SpecialityId);
    }
}