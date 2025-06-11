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

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var org = await _context.Organizations
            .Include(o => o.Departments)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (org == null) return NotFound();

        var departmentIds = org.Departments.Select(d => d.Id).ToList();

        var dto = new
        {
            Id = org.Id,
            Departments = departmentIds
        };

        return Ok(dto);
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