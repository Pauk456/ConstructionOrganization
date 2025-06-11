using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.Equipment;

[ApiController]
[Route("api/[controller]")]
public class EquipmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquipmentsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] Models.Equipment equipment)
    {
        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync();
        return Ok(equipment);
    }


    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var equipment = await _context.Equipments
            .Include(eq => eq.EquipmentObjectAssignments)
            .ThenInclude(eq => eq.Object)
            .FirstOrDefaultAsync(eq => eq.Id == id);

        if (equipment == null) return NotFound();

        var objectIds = equipment.EquipmentObjectAssignments.Select(eq => eq.ObjectId).ToList();

        var dto = new
        {
            Id = equipment.Id,
            AssignedObjectIds = objectIds
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Models.Equipment updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.Equipments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Оборудование не найдено");

        existing.ManagementId = updated.ManagementId;
        existing.EquipmentName = updated.EquipmentName;
        existing.EquipmentCount = updated.EquipmentCount;

        await _context.SaveChangesAsync();
        return Ok();
    }
}