using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers.People;

[ApiController]
[Route("api/[controller]")]
public class BrigadeMembersController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrigadeMembersController(AppDbContext context) => _context = context;

    [HttpPost("post")]
    public async Task<ActionResult> Create([FromBody] BrigadeMember member)
    {
        _context.BrigadeMembers.Add(member);
        await _context.SaveChangesAsync();
        return Ok(member);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] BrigadeMember updated)
    {
        if (id != updated.Id) return BadRequest();

        var existing = await _context.BrigadeMembers
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return NotFound("Член бригады не найден");

        existing.BrigadeId = updated.BrigadeId;
        existing.EmployeeId = updated.EmployeeId;

        await _context.SaveChangesAsync();
        return Ok();
    }
}