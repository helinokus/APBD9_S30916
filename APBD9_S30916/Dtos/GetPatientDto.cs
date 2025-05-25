namespace APBD9_S30916.Dtos;

public class GetPatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }

    public ICollection<PrescriptionDto> Prescriptions { get; set; }
}