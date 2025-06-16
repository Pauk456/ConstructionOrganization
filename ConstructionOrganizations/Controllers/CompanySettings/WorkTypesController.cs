using Microsoft.AspNetCore.Mvc;
using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class WorkTypesController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkTypesController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost("post")]
    public async Task<IActionResult> Create(WorkType workType)
    {
        _context.WorkTypes.Add(workType);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var workType = await _context.WorkTypes
            .Include(w => w.WorkSchedules)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workType == null) return NotFound();

        var scheduleIds = workType.WorkSchedules.Select(s => s.Id).ToList();

        var dto = new
        {
            Id = workType.Id,
            WorkScheduleIds = scheduleIds
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WorkType updatedType)
    {
        if (id != updatedType.Id) return BadRequest();

        var existing = await _context.WorkTypes.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Name = updatedType.Name;

        _context.WorkTypes.Update(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}