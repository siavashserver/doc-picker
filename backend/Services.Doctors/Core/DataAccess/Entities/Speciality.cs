using System.Text.Json.Serialization;

namespace Services.Doctors.Core.DataAccess.Entities;

public class Speciality
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public static string IndexName => "specialities";

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string SpecialityId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; set; }
}