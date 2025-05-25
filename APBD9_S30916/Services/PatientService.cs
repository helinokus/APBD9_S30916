using APBD9_S30916.Dtos;
using APBD9_S30916.Repositories;

namespace APBD9_S30916.Services;


public interface IPatientService
{
    Task<GetPatientDto?> GetPatientDtoAsync(int id);
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;

    public PatientService(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task<GetPatientDto?> GetPatientDtoAsync(int id)
    {
        if (id <= 0) return null;

        var patient = await _repo.GetPatientWithDetailsAsync(id);
        if (patient == null) return null;

        return new GetPatientDto
        {
            IdPatient = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.Id,
                        FirstName = p.Doctor.FirstName
                    },
                    Medicaments = p.Prescription_Medicaments.Select(pm => new MedicamentsDto
                    {
                        IdMedicament = pm.Medicament.Id,
                        Name = pm.Medicament.Name,
                        Description = pm.Medicament.Description,
                        Dose = pm.Dose
                    }).ToList()
                }).ToList()
        };
    }
}