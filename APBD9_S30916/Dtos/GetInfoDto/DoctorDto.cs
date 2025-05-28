using System.Text.Json.Serialization;

namespace APBD9_S30916.Dtos;

public class DoctorDto
{
    [JsonPropertyName("IdDoctor")]

    public int IdDoctor { get; set; }
    [JsonPropertyName("FirstName")]

    public string FirstName { get; set; }
}