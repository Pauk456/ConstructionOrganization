using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.Organizations;

[ApiController]
[Route("api/[controller]")]
public class ObjectAttributesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ObjectAttributesController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] ObjectAttribute attribute)
    {
        _context.ObjectAttributes.Add(attribute);
        await _context.SaveChangesAsync();
        return Ok(attribute);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] ObjectAttribute updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.ObjectAttributes
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Атрибут объекта не найден");

        existing.ObjectId = updated.ObjectId;
        existing.AttributeName = updated.AttributeName;
        existing.AttributeValue = updated.AttributeValue;

        await _context.SaveChangesAsync();
        return Ok();
    }
}