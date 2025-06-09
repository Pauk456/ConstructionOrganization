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
}
