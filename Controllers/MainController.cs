using Microsoft.AspNetCore.Mvc;
using ConstructionOrganizations.Models;
using System;

namespace ConstructionOrganizations.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    private readonly AppDbContext _context;

    public MainController(AppDbContext context)
    {
        _context = context;
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

    [HttpDelete("allTable")]
    private void ClearAllTables()
    {
        _context.BrigadeWorkAssignments.RemoveRange(_context.BrigadeWorkAssignments);
        _context.WorkSchedules.RemoveRange(_context.WorkSchedules);
        _context.MaterialUsages.RemoveRange(_context.MaterialUsages);
        _context.MaterialEstimates.RemoveRange(_context.MaterialEstimates);
        _context.EquipmentAssignments.RemoveRange(_context.EquipmentAssignments);
        _context.BrigadeMembers.RemoveRange(_context.BrigadeMembers);
        _context.ObjectAttributes.RemoveRange(_context.ObjectAttributes);
        _context.EmployeeAssignments.RemoveRange(_context.EmployeeAssignments);
        _context.Employees.RemoveRange(_context.Employees);
        _context.Brigades.RemoveRange(_context.Brigades);
        _context.Objects.RemoveRange(_context.Objects);
        _context.ObjectTypes.RemoveRange(_context.ObjectTypes);
        _context.Positions.RemoveRange(_context.Positions);
        _context.EmployeeTypes.RemoveRange(_context.EmployeeTypes);
        _context.Projects.RemoveRange(_context.Projects);
        _context.Departments.RemoveRange(_context.Departments);
        _context.Organizations.RemoveRange(_context.Organizations);

        _context.SaveChanges();
    }
}
