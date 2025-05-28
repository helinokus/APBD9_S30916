using APBD9_S30916.Exceptions;
using APBD9_S30916.Requests;
using APBD9_S30916.Responses;
using APBD9_S30916.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD9_S30916.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IPrescriptionService _prescriptionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription(AddPrescriptionRequest request)
    {
        try
        {
            await _prescriptionService.AddPrescription(request);
            return Ok("Prescription added");
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}