using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.Organizations;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProjectsController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] ConstructionProject project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] ConstructionProject updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.Projects
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Проект не найден");

        existing.Location = updated.Location;
        existing.DepartmentId = updated.DepartmentId;

        await _context.SaveChangesAsync();
        return Ok();
    }

    //[HttpPost("add-employee")]
    //public async Task<IActionResult> AddEmployee(int projectId, int employeeId)
    //{
    //    var brigade = await _context.Projects
    //        .Include(b => b.EmployeeAssignments)
    //        .FirstOrDefaultAsync(b => b.Id == projectId);

    //    var employee = await _context.Employees
    //        .FirstOrDefaultAsync(e => e.Id == employeeId);

    //    if (brigade == null || employee == null)
    //        return NotFound();

    //    var assignment = new EmployeeAssignment
    //    {
    //        ProjectId = projectId,
    //        EmployeeId = employeeId,
    //    };

    //    brigade.EmployeeAssignments.Add(assignment);
    //    await _context.SaveChangesAsync();

    //    return Ok();
    //}

    //[HttpDelete("remove-employee")]
    //public async Task<IActionResult> RemoveEmployee(int brigadeId, int employeeId)
    //{
    //    var brigade = await _context.Brigades
    //        .Include(b => b.Members)
    //        .FirstOrDefaultAsync(b => b.Id == brigadeId);

    //    if (brigade == null)
    //        return NotFound();

    //    var employee = brigade.Members.FirstOrDefault(e => e.EmployeeId == employeeId);

    //    if (employee == null)
    //        return NotFound();

    //    brigade.Members.Remove(employee);
    //    await _context.SaveChangesAsync();

    //    return Ok();
    //}
}