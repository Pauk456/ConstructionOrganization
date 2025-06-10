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