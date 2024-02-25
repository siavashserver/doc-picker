using Services.Doctors;

namespace WebAPI.REST.DTOs.Specialities;

public record GetSpecialitiesResponseDTO(bool HasNextPage, List<GetSpecialitiesResultDTO> Specialities)
{
    public static implicit operator GetSpecialitiesResponseDTO(GetSpecialitiesResponse source)
    {
        return new GetSpecialitiesResponseDTO(source.HasNextPage,
            source.Specialities.Select(specialitiesResult => new GetSpecialitiesResultDTO(specialitiesResult.Score,
                specialitiesResult.SpecialityId,
                specialitiesResult.Name, specialitiesResult.Description)).ToList());
    }
}