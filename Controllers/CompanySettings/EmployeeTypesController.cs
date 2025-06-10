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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeType employeeType)
    {
        _context.EmployeeTypes.Add(employeeType);
        await _context.SaveChangesAsync();
        return Ok();
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