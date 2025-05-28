using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APBD9_S30916.Dtos;

public class GetPatientDto
{
    [JsonPropertyName("IdPatient")]
    public int IdPatient { get; set; }
    [JsonPropertyName("FirstName")]

    public string FirstName { get; set; } = null!;
    [JsonPropertyName("LastName")]

    public string LastName { get; set; } = null!;
    [JsonPropertyName("BirthDate")]

    public DateOnly BirthDate { get; set; }
    [JsonPropertyName("Prescriptions")]


    public ICollection<PrescriptionDto> Prescriptions { get; set; }
}