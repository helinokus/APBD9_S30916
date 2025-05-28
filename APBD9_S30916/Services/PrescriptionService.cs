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
            Patient patient = await _patientRepository.GetPatientAsync(request.Patient.IdPatient);
            if (patient == null)
            {
                Patient newPatient = new Patient();
                //Проверить на автоайди 
                newPatient.FirstName = request.Patient.FirstName;
                newPatient.LastName = request.Patient.LastName;
                newPatient.BirthDate = request.Patient.BirthDate;
                await _prescriptionRepository.AddPatientAsync(newPatient);
            }

            Doctor doctor = await _prescriptionRepository.DoctorExistsAsync(request.Doctor.IdDoctor);
            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found");
            }

            foreach (MedicamentsAddDto medicament in request.Medicaments)
            {
                var medicamentExistsAsync =
                    await _prescriptionRepository.MedicamentExistsAsync(medicament.IdMedicament);
                if (medicamentExistsAsync == null)
                {
                    throw new NotFoundException($"Medicament {medicament.IdMedicament} not found");
                }
            }

            ;

            if (request.Medicaments.Count > 10)
            {
                throw new ConflictException("Medicament count can't be more than 10");
            }

            if (request.DueDate < request.Date)
            {
                throw new ConflictException("Date can't be greater than due date");
            }

            Prescription prescription = new Prescription
            {
                Patient = patient,
                Doctor = doctor,
                Date = request.Date,
                DueDate = request.DueDate,
                Prescription_Medicaments = new List<Prescription_Medicament>()
            };

            await _prescriptionRepository.AddPrescriptionAsync(prescription);

            var prescriptionMedicaments = request.Medicaments.Select(medicamentDto => 
                new Prescription_Medicament
                {
                    IdPrescription = prescription.IdPrescription, 
                    IdMedicament = medicamentDto.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Description
                }).ToList();

            await _prescriptionRepository.AddPrescriptionMedicamentsAsync(prescriptionMedicaments);

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw;
        }

        var responseEntityAddPrescription = new ResponseEntityAddPrescription();
        responseEntityAddPrescription.Response = "Add prescriptions success";
        return responseEntityAddPrescription;
    }
}