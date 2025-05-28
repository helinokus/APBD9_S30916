using System.Text.Json.Serialization;

namespace APBD9_S30916.Dtos;

public class PrescriptionDto
{
    [JsonPropertyName("IdPrescription")]
    public int IdPrescription { get; set; }
    [JsonPropertyName("Date")]

    public DateOnly Date { get; set; }
    [JsonPropertyName("DueDate")]

    public DateOnly DueDate { get; set; }
    [JsonPropertyName("Medicaments")]


    public ICollection<MedicamentsDto> Medicaments { get; set; } = new List<MedicamentsDto>();
    [JsonPropertyName("Doctor")]

    public DoctorDto Doctor { get; set; }
}