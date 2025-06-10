using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public class BrigadeWorkAssignmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrigadeWorkAssignmentsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] BrigadeWorkAssignment assignment)
    {
        _context.BrigadeWorkAssignments.Add(assignment);
        await _context.SaveChangesAsync();
        return Ok(assignment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] BrigadeWorkAssignment updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.BrigadeWorkAssignments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Назначение работы бригаде не найдено");

        existing.BrigadeId = updated.BrigadeId;
        existing.WorkScheduleId = updated.WorkScheduleId;

        await _context.SaveChangesAsync();
        return Ok();
    }
}