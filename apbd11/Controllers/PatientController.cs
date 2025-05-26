using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;

namespace apbd11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    
    private readonly IDbService _dbService;
    public PatientController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var result = await _dbService.getPatient(id);

        return Ok(result);
    }
}