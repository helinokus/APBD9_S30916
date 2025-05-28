using System.Text.Json.Serialization;

namespace APBD9_S30916.Dtos.AddPrescriptionDtos;

public class PatientAddDto
{
    [JsonPropertyName("IdPatient")]
    public int IdPatient { get; set; }
    [JsonPropertyName("FirstName")]

    public string FirstName { get; set; } = null!;
    [JsonPropertyName("LastName")]

    public string LastName { get; set; } = null!;
    [JsonPropertyName("BirthDate")]

    public DateOnly BirthDate { get; set; }
}