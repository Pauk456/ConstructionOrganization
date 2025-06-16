using Microsoft.AspNetCore.Mvc;
using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class EmployeeTypesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeTypesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("post")]
    public async Task<IActionResult> Create([FromBody] EmployeeType employeeType)
    {
        _context.EmployeeTypes.Add(employeeType);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var type = await _context.EmployeeTypes
            .Include(t => t.Employees)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (type == null) return NotFound();

        var employeeIds = type.Employees.Select(e => e.Id).ToList();

        var dto = new
        {
            Id = type.Id,
            Employees = employeeIds
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeType updatedType)
    {
        if (id != updatedType.Id) return BadRequest();

        var existing = await _context.EmployeeTypes.FindAsync(id);
        if (existing == null) return NotFound();

        existing.TypeName = updatedType.TypeName;

        _context.EmployeeTypes.Update(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}