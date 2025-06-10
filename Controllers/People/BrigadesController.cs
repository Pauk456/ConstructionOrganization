using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public class BrigadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrigadesController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] Brigade brigade)
    {
        _context.Brigades.Add(brigade);
        await _context.SaveChangesAsync();
        return Ok(brigade);
    }

    [HttpPost("add-member")]
    public async Task<IActionResult> AddMember(int brigadeId, int employeeId)
    {
        var brigade = await _context.Brigades
            .Include(b => b.Members)
            .FirstOrDefaultAsync(b => b.Id == brigadeId);

        var employee = await _context.Employees
            .Include(e => e.Brigades)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (brigade == null || employee == null)
            return NotFound();

        brigade.Members.Add(employee);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("remove-member")]
    public async Task<IActionResult> RemoveMember(int brigadeId, int employeeId)
    {
        var brigade = await _context.Brigades
            .Include(b => b.Members)
            .FirstOrDefaultAsync(b => b.Id == brigadeId);

        if (brigade == null)
            return NotFound();

        var employee = brigade.Members.FirstOrDefault(e => e.Id == employeeId);
        if (employee == null)
            return NotFound();

        brigade.Members.Remove(employee);
        await _context.SaveChangesAsync();
        return Ok();
    }
}