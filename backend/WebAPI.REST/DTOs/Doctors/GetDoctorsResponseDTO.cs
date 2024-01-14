using Services.Doctors;

namespace WebAPI.REST.DTOs.Doctors;

public record GetDoctorsResponseDTO(bool HasNextPage, List<GetDoctorsResultDTO> Doctors)
{
    public static implicit operator GetDoctorsResponseDTO(GetDoctorsResponse source)
    {
        return new GetDoctorsResponseDTO(source.HasNextPage,
            source.Doctors.Select(doctorsResult => new GetDoctorsResultDTO(doctorsResult.Score, doctorsResult.DoctorId,
                doctorsResult.Name, doctorsResult.SpecialityId)).ToList());
    }
}