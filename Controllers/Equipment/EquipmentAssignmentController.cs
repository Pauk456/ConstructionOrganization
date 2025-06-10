using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.Equipment;

[ApiController]
[Route("api/[controller]")]
public class EquipmentAssignmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquipmentAssignmentsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] EquipmentAssignment assignment)
    {
        _context.EquipmentAssignments.Add(assignment);
        await _context.SaveChangesAsync();
        return Ok(assignment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] EquipmentAssignment updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.EquipmentAssignments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Назначение оборудования не найдено");

        existing.EquipmentId = updated.EquipmentId;
        existing.ObjectId = updated.ObjectId;
        existing.AssignedDate = updated.AssignedDate;
        existing.ReturnedDate = updated.ReturnedDate;

        await _context.SaveChangesAsync();
        return Ok();
    }
}