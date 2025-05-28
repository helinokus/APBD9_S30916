using APBD9_S30916.Data;
using APBD9_S30916.Dtos;
using APBD9_S30916.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD9_S30916.Repositories;

public interface IPatientRepository
{
    Task<GetPatientDto?> GetPatientWithDetailsAsync(int id);
    Task<Patient?> GetPatientAsync(int id);
    Task AddPatientAsync(Patient patient);
}

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetPatientDto?> GetPatientWithDetailsAsync(int id)
    {
        return await _context.Patients
            .Where(p => p.Id == id)
            .Select(p => new GetPatientDto
            {
                IdPatient = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = pr.Doctor.Id,
                            FirstName = pr.Doctor.FirstName,
                        },
                        Medicaments = pr.Prescription_Medicaments
                            .Select(pm => new MedicamentsDto
                                {
                                    IdMedicament = pm.IdMedicament,
                                    Name = pm.Medicament.Name,
                                    Dose = pm.Dose,
                                    Description = pm.Details
                                })
                            .ToList()
                        
                    }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<Patient?> GetPatientAsync(int id)
    {
        return await _context.Patients.Where(p => p.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
    }

}