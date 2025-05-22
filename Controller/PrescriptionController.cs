using apbd_tutorial11.DTO;
using apbd_tutorial11.Exceptions;
using apbd_tutorial11.Model;
using apbd_tutorial11.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial11.Controller;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController:ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PrescriptionDTO prescription)
    {
        try
        {
            await _prescriptionService.AddPrescription(prescription);

        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        return Created();
    }
}