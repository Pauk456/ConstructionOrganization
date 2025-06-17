using ConstructionOrganizations.Models;
using ConstructionOrganizations.DTOs;
using ConstructionOrganizations.Services.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionOrganizations.Controllers.People
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var dto = await _employeeService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        [Authorize]
        [HttpPost("post")]
        public async Task<ActionResult<Employee>> Create([FromBody] Employee employee)
        {
            var createdEmployee = await _employeeService.CreateAsync(employee);
            return Ok(createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Employee employee)
        {
            try
            {
                await _employeeService.UpdateAsync(id, employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll(int count = 2)
        {
            var employees = await _employeeService.GetAllAsync(count);
            return Ok(employees);
        }
    }
}