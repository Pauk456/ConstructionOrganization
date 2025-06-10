using Microsoft.AspNetCore.Mvc;
using ConstructionOrganizations.Models;
using System;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Security.Principal;
using System.Reflection;

namespace ConstructionOrganizations.Controllers;

[ApiController]
[Route("api/")]
public class MainController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly Dictionary<string, IQueryable<System.Object>> entityMap;

    public MainController(AppDbContext context)
    {
        _context = context;

        entityMap = new()
        {
            ["employeetypes"] = _context.EmployeeTypes.AsQueryable(),
            ["positions"] = _context.Positions.AsQueryable(),
            ["objecttypes"] = _context.ObjectTypes.AsQueryable(),
            ["worktypes"] = _context.WorkTypes.AsQueryable(),
            ["organizations"] = _context.Organizations.AsQueryable(),
            ["departments"] = _context.Departments.AsQueryable(),
            ["projects"] = _context.Projects.AsQueryable(),
            ["employees"] = _context.Employees.AsQueryable(),
            ["brigades"] = _context.Brigades.AsQueryable(),
            ["objects"] = _context.Objects.AsQueryable(),
            ["objectattributes"] = _context.ObjectAttributes.AsQueryable(),
            ["workschedules"] = _context.WorkSchedules.AsQueryable(),
            ["materialestimates"] = _context.MaterialEstimates.AsQueryable(),
            ["materialusages"] = _context.MaterialUsages.AsQueryable(),
            ["equipments"] = _context.Equipments.AsQueryable()
        };

    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAll(string entity)
    {
        if (!entityMap.TryGetValue(entity.ToLower(), out var query))
            return BadRequest($"Unknown entity: {entity}");

        var data = await query.ToListAsync();

        return Ok(data);
    }

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id, string entity)
    {
        if (!entityMap.TryGetValue(entity.ToLower(), out var query))
            return BadRequest($"Unknown entity: {entity}");

        var item = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        return Ok(item);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id, string entity)
    {
        if (!entityMap.TryGetValue(entity.ToLower(), out var query))
            return BadRequest($"Unknown entity: {entity}");

        var item = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        if (item == null) return BadRequest();

        _context.Remove(item);

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("testData")]
    public IActionResult TestData()
    {
        try
        {
            var org = new ConstructionOrganization { Name = "dfsg" };
            _context.Organizations.Add(org);
            _context.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ошибка при добавлении данных", Error = ex.Message });
        }
    }
}
