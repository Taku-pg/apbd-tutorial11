using apbd_tutorial11.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial11.Controller;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var res= await _patientService.GetPatient(id);
        if (res == null)
            return NotFound();
        
            
        return Ok(res);
    }
}