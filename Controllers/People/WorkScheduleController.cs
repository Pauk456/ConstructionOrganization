using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public class WorkSchedulesController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkSchedulesController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] WorkSchedule schedule)
    {
        _context.WorkSchedules.Add(schedule);
        await _context.SaveChangesAsync();
        return Ok(schedule);
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var schedule = await _context.WorkSchedules
            .Include(s => s.MaterialUsages)
            .Include(s => s.BrigadeWorkAssignments)
            .ThenInclude(bwa => bwa.Brigade)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (schedule == null) return NotFound();

        var materialUsageIds = schedule.MaterialUsages.Select(m => m.Id).ToList();
        var brigadeIds = schedule.BrigadeWorkAssignments.Select(bwa => bwa.BrigadeId).ToList();

        var dto = new
        {
            Id = schedule.Id,
            MaterialUsageIds = materialUsageIds,
            BrigadeIds = brigadeIds
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] WorkSchedule updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.WorkSchedules
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("График работ не найден");

        existing.ObjectId = updated.ObjectId;
        existing.WorkTypeId = updated.WorkTypeId;
        existing.PlannedStartDate = updated.PlannedStartDate;
        existing.PlannedEndDate = updated.PlannedEndDate;

        await _context.SaveChangesAsync();
        return Ok();
    }
}