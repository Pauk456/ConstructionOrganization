using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.Equipment;

[ApiController]
[Route("api/[controller]")]
public class MaterialEstimatesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MaterialEstimatesController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] MaterialEstimate estimate)
    {
        _context.MaterialEstimates.Add(estimate);
        await _context.SaveChangesAsync();
        return Ok(estimate);
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var estimate = await _context.MaterialEstimates
            .FirstOrDefaultAsync(m => m.Id == id);

        if (estimate == null) return NotFound();

        var dto = new
        {
            Id = estimate.Id,
            ObjectId = estimate.ObjectId
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] MaterialEstimate updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.MaterialEstimates
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Оценка материалов не найдена");

        existing.ObjectId = updated.ObjectId;
        existing.MaterialName = updated.MaterialName;
        existing.MaterialCount = updated.MaterialCount;

        await _context.SaveChangesAsync();
        return Ok();
    }
}