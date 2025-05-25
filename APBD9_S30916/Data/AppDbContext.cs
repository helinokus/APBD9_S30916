using APBD9_S30916.Dtos;
using APBD9_S30916.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD9_S30916.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var patient = new Patient
        {
            Id = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            BirthDate = new DateOnly(1980, 5, 12)
        };

        var doctor = new Doctor
        {
            Id = 1,
            FirstName = "Anna",
            LastName = "Nowak",
            Email = "anna.nowak@clinic.pl"
        };

        var medicament = new Medicament
        {
            Id = 1,
            Name = "Ibuprofen",
            Description = "Pain relief",
            Type = "Tablet"
        };

        var prescription = new Prescription
        {
            IdPrescription = 1,
            Date = new DateOnly(2024, 5, 1),
            DueDate = new DateOnly(2024, 6, 1),
            IdPatient = patient.Id,
            IdDoctor = doctor.Id
        };

        var prescMed = new Prescription_Medicament
        {
            IdMedicament = medicament.Id,
            IdPrescription = prescription.IdPrescription,
            Dose = 2,
            Details = "Take twice daily"
        };

        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<Prescription_Medicament>().HasData(prescMed);
    }
}