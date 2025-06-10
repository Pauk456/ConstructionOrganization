using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public class EmployeeAssignmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeAssignmentsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] EmployeeAssignment assignment)
    {
        _context.EmployeeAssignments.Add(assignment);
        await _context.SaveChangesAsync();
        return Ok(assignment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] EmployeeAssignment updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.EmployeeAssignments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Назначение сотрудника не найдено");

        existing.EmployeeId = updated.EmployeeId;
        existing.ProjectId = updated.ProjectId;

        await _context.SaveChangesAsync();
        return Ok();
    }
}