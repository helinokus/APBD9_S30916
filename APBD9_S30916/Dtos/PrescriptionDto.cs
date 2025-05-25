namespace APBD9_S30916.Dtos;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }

    public ICollection<MedicamentsDto> Medicaments { get; set; } = new List<MedicamentsDto>();
    public DoctorDto Doctor { get; set; }
}