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
        schedule.PlannedStartDate = DateTime.SpecifyKind(schedule.PlannedStartDate, DateTimeKind.Utc);
        schedule.PlannedEndDate = DateTime.SpecifyKind(schedule.PlannedEndDate, DateTimeKind.Utc);

        if (schedule.ActualStartDate != null)
            schedule.ActualStartDate = DateTime.SpecifyKind(schedule.ActualStartDate.Value, DateTimeKind.Utc);

        if (schedule.ActualEndDate != null)
            schedule.ActualEndDate = DateTime.SpecifyKind(schedule.ActualEndDate.Value, DateTimeKind.Utc);

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
            schedule.Id,

            schedule.PlannedStartDate,
            schedule.PlannedEndDate,
            schedule.ActualStartDate,
            schedule.ActualEndDate,

            ObjectId = schedule.ObjectId,

            WorkTypeId = schedule.WorkTypeId,
            WorkTypeName = schedule.WorkType?.Name,

            MaterialUsageIds = schedule.MaterialUsages.Select(m => m.Id).ToList(),

            BrigadeIds = schedule.BrigadeWorkAssignments
            .Select(x => x.BrigadeId)
            .ToList()
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
        existing.ActualStartDate = updated.ActualStartDate;
        existing.ActualEndDate = updated.ActualEndDate;

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("assignBrigade")]
    public async Task<IActionResult> AssignBrigade(int workScheduleId, int brigadeId)
    {
        var schedule = await _context.WorkSchedules
            .Include(s => s.BrigadeWorkAssignments)
            .FirstOrDefaultAsync(s => s.Id == workScheduleId);

        var brigade = await _context.Brigades
            .FirstOrDefaultAsync(b => b.Id == brigadeId);

        if (schedule == null || brigade == null)
            return NotFound();

        schedule.BrigadeWorkAssignments.Add(new BrigadeWorkAssignment
        {
            WorkScheduleId = workScheduleId,
            BrigadeId = brigadeId
        });

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("unassignBarigade")]
    public async Task<IActionResult> UnassignBrigade(int workScheduleId, int brigadeId)
    {
        var schedule = await _context.WorkSchedules
            .Include(s => s.BrigadeWorkAssignments)
            .FirstOrDefaultAsync(s => s.Id == workScheduleId);

        var brigade = schedule.BrigadeWorkAssignments
            .FirstOrDefault(s => s.BrigadeId == brigadeId);


        if (schedule == null || brigade == null)
            return NotFound();


        schedule.BrigadeWorkAssignments.Remove(brigade);
        await _context.SaveChangesAsync();

        return Ok();
    }
}