using Microsoft.AspNetCore.Mvc;
using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class WorkTypesController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkTypesController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> Create(WorkType workType)
    {
        _context.WorkTypes.Add(workType);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WorkType updatedType)
    {
        if (id != updatedType.Id) return BadRequest();

        var existing = await _context.WorkTypes.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Name = updatedType.Name;

        _context.WorkTypes.Update(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}