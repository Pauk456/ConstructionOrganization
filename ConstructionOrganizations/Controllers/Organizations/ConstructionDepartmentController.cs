using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.Organizations;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] ConstructionDepartment dept)
    {
        _context.Departments.Add(dept);
        await _context.SaveChangesAsync();
        return Ok(dept);
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var dept = await _context.Departments
            .Include(d => d.Projects)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dept == null) return NotFound();

        var projectIds = dept.Projects.Select(p => p.Id).ToList();

        var dto = new
        {
            Id = dept.Id,
            Projects = projectIds
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] ConstructionDepartment updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.Departments
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Департамент не найден");

        existing.Name = updated.Name;
        existing.OrganizationId = updated.OrganizationId;

        await _context.SaveChangesAsync();
        return Ok();
    }
}