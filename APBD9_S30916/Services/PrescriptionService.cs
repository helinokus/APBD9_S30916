using APBD9_S30916.Dtos.AddPrescriptionDtos;
using APBD9_S30916.Exceptions;
using APBD9_S30916.Models;
using APBD9_S30916.Repositories;
using APBD9_S30916.Requests;
using APBD9_S30916.Responses;

namespace APBD9_S30916.Services;

public interface IPrescriptionService
{
    Task<ResponseEntityAddPrescription> AddPrescription(AddPrescriptionRequest request);
}

public class PrescriptionService(IPrescriptionRepository _prescriptionRepository, IPatientRepository _patientRepository) : IPrescriptionService
{
    public async Task<ResponseEntityAddPrescription> AddPrescription(AddPrescriptionRequest request)
    {
        await using var transaction = await _prescriptionRepository.BeginTransactionAsync();

        try
        {
            Patient? patient = await _patientRepository.GetPatientAsync(request.Patient.IdPatient);
            if (patient == null)
            {
                Patient newPatient = new Patient
                {
                    FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                    BirthDate = request.Patient.BirthDate,
                };
                await _patientRepository.AddPatientAsync(newPatient);
                await _prescriptionRepository.SaveChangesAsync(); 
                patient = newPatient;
            }

            Doctor? doctor = await _prescriptionRepository.GetDoctorAsync(request.Doctor.IdDoctor);
            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found");
            }
            foreach (MedicamentsAddDto medicament in request.Medicaments)
            {
                var medicamentExistsAsync =
                    await _prescriptionRepository.GetMedicamentAsync(medicament.IdMedicament);
                if (medicamentExistsAsync == null)
                {
                    throw new NotFoundException($"Medicament {medicament.IdMedicament} not found");
                }
            }

            if (request.Medicaments.Count > 10)
            {
                throw new ConflictException("Medicament count can't be more than 10");
            }

            if (request.Medicaments.Count == 0)
            {
                throw new ConflictException("Medicaments count can't be 0");
            }

            if (request.DueDate < request.Date)
            {
                throw new ConflictException("Date can't be greater than due date");
            }
            

            Prescription prescription = new Prescription
            {
                IdPatient = patient.Id,
                IdDoctor = request.Doctor.IdDoctor,
                Date = request.Date,
                DueDate = request.DueDate,
                Prescription_Medicaments = new List<Prescription_Medicament>()
            };

            await _prescriptionRepository.AddPrescriptionAsync(prescription);
            await _prescriptionRepository.SaveChangesAsync(); 


            var prescriptionMedicaments = request.Medicaments.Select(medicamentDto => 
                new Prescription_Medicament
                {
                    IdPrescription = prescription.IdPrescription, 
                    IdMedicament = medicamentDto.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Description
                }).ToList();
            await _prescriptionRepository.AddPrescriptionMedicamentsAsync(prescriptionMedicaments);
            await _prescriptionRepository.SaveChangesAsync(); 



            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return new ResponseEntityAddPrescription
        {
            Response = "Add prescriptions success"
        };
    }
}