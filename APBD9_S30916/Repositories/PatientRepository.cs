using APBD9_S30916.Data;
using APBD9_S30916.Dtos;
using APBD9_S30916.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD9_S30916.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetPatientWithDetailsAsync(int id);
}

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Patient?> GetPatientWithDetailsAsync(int id)
    {
        return await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pres => pres.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pres => pres.Prescription_Medicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}