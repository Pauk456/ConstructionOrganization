using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public class BrigadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrigadesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] Employee employee)
    {
        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Employee employee)
    {
        if (id != employee.Id) return BadRequest();

        var existingEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existingEmployee == null)
        {
            return NotFound("Сотрудник не найден.");
        }

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.EmployeeTypeId = employee.EmployeeTypeId;
        existingEmployee.PositionId = employee.PositionId;

        await _context.SaveChangesAsync();

        return Ok();
    }
}