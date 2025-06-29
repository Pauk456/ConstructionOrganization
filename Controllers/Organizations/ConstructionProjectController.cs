﻿using ConstructionOrganizations.Models;
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

    [HttpGet("get")]
    public async Task<ActionResult> GetById(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Employees)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null) return NotFound();

        var employeeIds = project.Employees.Select(e => e.Id).ToList();

        var dto = new
        {
            Id = project.Id,
            Employees = employeeIds
        };

        return Ok(dto);
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
}