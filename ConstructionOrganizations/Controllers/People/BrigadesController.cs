using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var brigade = await _context.Brigades
            .Include(b => b.Members)
            .Include(b => b.BrigadeWorkAssignments)
            .ThenInclude(bwa => bwa.WorkSchedule)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (brigade == null) return NotFound();

        var memberIds = brigade.Members.Select(m => m.Id).ToList();
        var workScheduleIds = brigade.BrigadeWorkAssignments.Select(bwa => bwa.WorkScheduleId).ToList();

        var dto = new
        {
            Id = brigade.Id,
            Members = memberIds,
            WorkScheduleIds = workScheduleIds
        };

        return Ok(dto);
    }

}