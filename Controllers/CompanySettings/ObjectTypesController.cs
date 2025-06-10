using Microsoft.AspNetCore.Mvc;
using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class ObjectTypesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ObjectTypesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ObjectType objectType)
    {
        _context.ObjectTypes.Add(objectType);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ObjectType updatedType)
    {
        if (id != updatedType.Id) return BadRequest();

        var existing = await _context.ObjectTypes.FindAsync(id);
        if (existing == null) return NotFound();

        existing.TypeName = updatedType.TypeName;

        _context.ObjectTypes.Update(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}