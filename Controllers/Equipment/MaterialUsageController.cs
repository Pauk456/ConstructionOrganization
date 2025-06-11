using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.Equipment;

[ApiController]
[Route("api/[controller]")]
public class MaterialUsagesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MaterialUsagesController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] MaterialUsage usage)
    {
        _context.MaterialUsages.Add(usage);
        await _context.SaveChangesAsync();
        return Ok(usage);
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var usage = await _context.MaterialUsages
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usage == null) return NotFound();

        var dto = new
        {
            Id = usage.Id,
            ObjectId = usage.ObjectId,
            WorkScheduleId = usage.WorkScheduleId
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] MaterialUsage updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.MaterialUsages
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Использование материала не найдено");

        existing.ObjectId = updated.ObjectId;
        existing.WorkScheduleId = updated.WorkScheduleId;
        existing.MaterialName = updated.MaterialName;
        existing.MaterialCount = updated.MaterialCount;

        await _context.SaveChangesAsync();
        return Ok();
    }
}