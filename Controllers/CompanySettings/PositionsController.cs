using Microsoft.AspNetCore.Mvc;
using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PositionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Position position)
    {
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Position updatedPos)
    {
        if (id != updatedPos.Id) return BadRequest();

        var existing = await _context.Positions.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Name = updatedPos.Name;

        _context.Positions.Update(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}