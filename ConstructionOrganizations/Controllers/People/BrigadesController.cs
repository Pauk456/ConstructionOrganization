using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ConstructionOrganizations.Controllers.People;

using ConstructionOrganizations.Services.People;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class BrigadesController : ControllerBase
{
    private readonly BrigadeService _brigadeService;

    public BrigadesController(BrigadeService brigadeService)
    {
        _brigadeService = brigadeService;
    }

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] Brigade brigade)
    {
        var createdBrigade = await _brigadeService.CreateAsync(brigade);
        return Ok(createdBrigade);
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _brigadeService.GetByIdAsync(id);
        if (result == null) return NotFound();

        return Ok(result);
    }
}
