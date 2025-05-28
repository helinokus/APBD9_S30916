using APBD9_S30916.Dtos.AddPrescriptionDtos;

namespace APBD9_S30916.Requests;

public class AddPrescriptionRequest
{
    public PatientAddDto Patient { get; set; }
    public List<MedicamentsAddDto> Medicaments { get; set; }
    public DoctorAddDto Doctor { get; set; }
    
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly DueDate { get; set; }
}