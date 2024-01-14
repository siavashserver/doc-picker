using Services.Doctors;

namespace WebAPI.REST.DTOs.Specialities;

public record CreateSpecialityRequestDTO(string Name, string Description)
{
    public static implicit operator CreateSpecialityRequest(CreateSpecialityRequestDTO source)
    {
        return new CreateSpecialityRequest
        {
            Name = source.Name,
            Description = source.Description
        };
    }
}