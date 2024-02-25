using Services.Doctors;

namespace WebAPI.REST.DTOs.Doctors;

public record GetDoctorResponseDTO(string DoctorId, string Name, string SpecialityId)
{
    public static implicit operator GetDoctorResponseDTO(GetDoctorsResponse source)
    {
        var doctor = source.Doctors[0];
        return new GetDoctorResponseDTO(doctor.DoctorId, doctor.Name, doctor.SpecialityId);
    }
}