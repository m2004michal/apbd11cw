using apbd11.DTOs;
using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;

namespace apbd11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase {
    
    private readonly IDbService _dbService;
    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }
        
    [HttpPost("create")]
    public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionDTO createPrescription){
        try {
            await _dbService.createPrescription(createPrescription);
            return Ok("Prescription added successfully");
        } catch (ArgumentException e) {
            return BadRequest(e.Message);
        }
    }
}