using Services.Doctors;

namespace WebAPI.REST.DTOs.Specialities;

public record GetSpecialityResponseDTO(string SpecialityId, string Name, string Description)
{
    public static implicit operator GetSpecialityResponseDTO(GetSpecialitiesResponse source)
    {
        var speciality = source.Specialities[0];
        return new GetSpecialityResponseDTO(speciality.SpecialityId, speciality.Name, speciality.Description);
    }
}