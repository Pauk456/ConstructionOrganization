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
    public async Task<ActionResult> Create([FromBody] Equipment equipment)
    {
        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync();
        return Ok(equipment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Equipment updated)
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