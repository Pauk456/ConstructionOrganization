using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Controllers;

[ApiController]
[Route("api/request")]
public class RequestsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RequestsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("1")]
    public async Task<IActionResult> First()
    {
        var projects = _context.Projects
            .Include(x => x.Employees)
            .ThenInclude(x => x.Position).AsQueryable();

        var answer = projects
            .SelectMany(p => p.Employees
                                    .Where(e => e.Position.Name == "Начальник участка")
                                    .Select(e => new { ProjectId = p.Id, EmployeeId = e.Id }));

        return Ok(answer);
    }

    [HttpGet("2")]
    public async Task<IActionResult> Second()
    {
        var projects = _context.Projects
            .Include(x => x.Employees).ThenInclude(x => x.Position)
            .Include(x => x.Employees).ThenInclude(e => e.EmployeeType);


        var answer = projects
            .SelectMany(p => p.Employees
                                    .Where(e => e.EmployeeType.TypeName == "Инженер")
                                    .Select(e => new { ProjectId = p.Id, EmployeeId = e.Id, PosName = e.Position.Name }));

        return Ok(answer);
    }

    [HttpGet("3")]
    public async Task<IActionResult> GetProjectsAndWorkSchedules(int ProjectId)
    {
        var result = await _context.Projects
            .Where(d => d.Id == ProjectId)
            .Include(d => d.Objects)
            .SelectMany(d => d.Objects) // Все проекты этого отдела
            .Select(project => new
            {
                ProjectId = project.Id,
                Schedule = project.WorkSchedules.ToArray() 
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("4")]
    public async Task<IActionResult> GetBrigadeWorkersByObject(int ObjectId)
    {
        var result = await _context.Objects
            .Where(d => d.Id == ObjectId)
            .Include(d => d.WorkSchedules)
            .ThenInclude(d => d.BrigadeWorkAssignments)
            .SelectMany(d => d.WorkSchedules)
            .SelectMany(d => d.BrigadeWorkAssignments)
            .Select(d => new
            {
                ObjectId = ObjectId,
                BrigadeId = d.BrigadeId
            })
            .ToListAsync();

        return Ok(result);
    }
}

