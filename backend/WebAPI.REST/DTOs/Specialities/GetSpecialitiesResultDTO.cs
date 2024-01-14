namespace WebAPI.REST.DTOs.Specialities;

public record GetSpecialitiesResultDTO(float? Score, string SpecialityId, string Name, string Description)
{
}