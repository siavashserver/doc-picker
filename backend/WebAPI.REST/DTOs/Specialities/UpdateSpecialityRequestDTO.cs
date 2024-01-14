using Services.Doctors;

namespace WebAPI.REST.DTOs.Specialities;

public record UpdateSpecialityRequestDTO(string Name, string Description)
{
    public static implicit operator UpdateSpecialityRequest(UpdateSpecialityRequestDTO source)
    {
        return new UpdateSpecialityRequest
        {
            SpecialityId = default,
            Name = source.Name,
            Description = source.Description
        };
    }
}