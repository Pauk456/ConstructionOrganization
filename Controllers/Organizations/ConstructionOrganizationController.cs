using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrganizationsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] ConstructionOrganization org)
    {
        _context.Organizations.Add(org);
        await _context.SaveChangesAsync();
        return Ok(org);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] ConstructionOrganization updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.Organizations
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Организация не найдена");

        existing.Name = updated.Name;

        await _context.SaveChangesAsync();
        return Ok();
    }
}