using APBD9_S30916.Data;
using APBD9_S30916.Dtos;
using APBD9_S30916.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD9_S30916.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _pservice;

    public PatientsController(IPatientService pservice)
    {
        _pservice = pservice;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var result = await _pservice.GetPatientDtoAsync(id);
        if (result == null)
            return NotFound("Patient not found or invalid ID");

        return Ok(result);
    }
}