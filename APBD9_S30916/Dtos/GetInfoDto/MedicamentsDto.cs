using System.Text.Json.Serialization;

namespace APBD9_S30916.Dtos;

public class MedicamentsDto
{
    [JsonPropertyName("IdMedicament")]

    public int IdMedicament { get; set; }
    [JsonPropertyName("Name")]

    public string Name { get; set; }
    [JsonPropertyName("Dose")]

    public int? Dose { get; set; }
    [JsonPropertyName("Description")]

    public string Description { get; set; }
    
}