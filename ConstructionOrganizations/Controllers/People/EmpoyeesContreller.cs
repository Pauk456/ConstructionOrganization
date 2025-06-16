using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ConstructionOrganizations.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.ConstructionProject)
            .Include(e => e.Brigade)
            .Include(e => e.EmployeeType)
            .Include(e => e.Position)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null) return NotFound();

        var dto = new
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            ProjectId = employee.ConstructionProject?.Id,
            BrigadeId = employee.Brigade?.Id,
            EmployeeTypeId = employee.EmployeeType?.Id,
            PositionId = employee.Position?.Id
        };

        return Ok(dto);
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
        existingEmployee.ProjectId = employee.ProjectId;
        existingEmployee.BrigadeId = employee.Brigade?.Id;

        await _context.SaveChangesAsync();

        return Ok();
    }
}