using System.Text.Json.Serialization;

namespace Services.Doctors.Core.DataAccess.Entities;

public class Doctor
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public static string IndexName => "doctors";

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string DoctorId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SpecialityId { get; set; }
}