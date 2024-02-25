using Services.Doctors;

namespace WebAPI.REST.DTOs.Doctors;

public record GetDoctorsRequestDTO(
    int Page,
    int PageSize,
    List<string> DoctorIds,
    List<string> Names,
    List<string> SpecialityIds)
{
    public static implicit operator GetDoctorsRequest(GetDoctorsRequestDTO source)
    {
        return new GetDoctorsRequest
        {
            Page = source.Page,
            PageSize = source.PageSize,
            DoctorIds = { source.DoctorIds },
            Names = { source.Names },
            SpecialityIds = { source.SpecialityIds }
        };
    }
}