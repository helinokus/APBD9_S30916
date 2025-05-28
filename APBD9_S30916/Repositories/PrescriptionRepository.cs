using APBD9_S30916.Data;
using APBD9_S30916.Models;
using APBD9_S30916.Requests;
using APBD9_S30916.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APBD9_S30916.Repositories;

public interface IPrescriptionRepository
{
    Task<Doctor?> GetDoctorAsync(int idDoctor);
    Task<Medicament?> GetMedicamentAsync(int idMedicament);
    Task AddPrescriptionAsync(Prescription prescription);
    Task AddPrescriptionMedicamentsAsync(IEnumerable<Prescription_Medicament> prescriptionMedicaments);
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task SaveChangesAsync();
} 

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly AppDbContext _context;

    public PrescriptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> GetDoctorAsync(int idDoctor)
    {
        return await _context.Doctors.Where(d => d.Id == idDoctor).FirstOrDefaultAsync();
    }


    public async Task<Medicament?> GetMedicamentAsync(int idMedicament)
    {
        return await _context.Medicaments.Where(m => m.Id == idMedicament).FirstOrDefaultAsync();
    }
    

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
    }

    public async Task AddPrescriptionMedicamentsAsync(IEnumerable<Prescription_Medicament> prescriptionMedicaments)
    {
        await _context.Prescription_Medicaments.AddRangeAsync(prescriptionMedicaments);
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}