using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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