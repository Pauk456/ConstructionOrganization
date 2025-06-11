using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Object = ConstructionOrganizations.Models.Object;


namespace ConstructionOrganizations.Controllers.Organizations;

[ApiController]
[Route("api/[controller]")]
public class ObjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ObjectsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] Object obj)
    {
        _context.Objects.Add(obj);
        await _context.SaveChangesAsync();
        return Ok(obj);
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var obj = await _context.Objects
            .Include(o => o.WorkSchedules)
            .Include(o => o.MaterialEstimates)
            .Include(o => o.Attributes)
            .Include(o => o.MaterialUsages)
            .Include(o => o.EquipmentObjectAssignments)
            .ThenInclude(eq => eq.Equipment)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (obj == null) return NotFound();

        var dto = new
        {
            Id = obj.Id,
            WorkScheduleIds = obj.WorkSchedules.Select(w => w.Id).ToList(),
            MaterialEstimateIds = obj.MaterialEstimates.Select(m => m.Id).ToList(),
            AttributeIds = obj.Attributes.Select(a => a.Id).ToList(),
            MaterialUsageIds = obj.MaterialUsages.Select(m => m.Id).ToList(),
            EquipmentIds = obj.EquipmentObjectAssignments.Select(eq => eq.EquipmentId).ToList()
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Object updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.Objects
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Объект не найден");

        existing.ConstructionProjectId = updated.ConstructionProjectId;
        existing.ObjectType = updated.ObjectType;
        existing.StartDate = updated.StartDate;
        existing.EndDate = updated.EndDate;
        existing.Status = updated.Status;

        await _context.SaveChangesAsync();
        return Ok();
    }
}