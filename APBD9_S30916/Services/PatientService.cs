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

        return patient;

    }
}