using Services.Doctors;

namespace WebAPI.REST.DTOs.Specialities;

public record GetSpecialitiesRequestDTO(
    int Page,
    int PageSize,
    List<string> SpecialityIds,
    List<string> Names,
    List<string> Descriptions)
{
    public static implicit operator GetSpecialitiesRequest(GetSpecialitiesRequestDTO source)
    {
        return new GetSpecialitiesRequest
        {
            Page = source.Page,
            PageSize = source.PageSize,
            SpecialityIds = { source.SpecialityIds },
            Names = { source.Names },
            Descriptions = { source.Descriptions }
        };
    }
}