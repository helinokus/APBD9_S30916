using APBD9_S30916.Data;
using APBD9_S30916.Models;
using APBD9_S30916.Requests;
using APBD9_S30916.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APBD9_S30916.Repositories;

public interface IPrescriptionRepository
{
    Task<Patient?> PatientExistsAsync(int idPatient);
    Task<Doctor?> DoctorExistsAsync(int idDoctor);
    Task<Medicament?> MedicamentExistsAsync(int idMedicament);
    Task AddPatientAsync(Patient patient);
    Task AddPrescriptionAsync(Prescription prescription);
    Task AddPrescriptionMedicamentsAsync(IEnumerable<Prescription_Medicament> prescriptionMedicaments);
    Task<IDbContextTransaction> BeginTransactionAsync();
} 

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly AppDbContext _context;

    public PrescriptionRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Patient?> PatientExistsAsync(int idPatient)
    {
        return await _context.Patients.Where(p => p.Id == idPatient).FirstOrDefaultAsync();
    }

    public async Task<Doctor?> DoctorExistsAsync(int idDoctor)
    {
        return await _context.Doctors.Where(d => d.Id == idDoctor).FirstOrDefaultAsync();
    }


    public async Task<Medicament?> MedicamentExistsAsync(int idMedicament)
    {
        return await _context.Medicaments.Where(m => m.Id == idMedicament).FirstOrDefaultAsync();
    }

    public async Task AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task AddPrescriptionMedicamentsAsync(IEnumerable<Prescription_Medicament> prescriptionMedicaments)
    {
        await _context.Prescription_Medicaments.AddRangeAsync(prescriptionMedicaments);
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
    
    
}