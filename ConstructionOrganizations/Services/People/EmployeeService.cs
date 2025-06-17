using ConstructionOrganizations.DTOs;
using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionOrganizations.Services.People
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.ConstructionProject)
                .Include(e => e.Brigade)
                .Include(e => e.EmployeeType)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ProjectId = employee.ConstructionProject?.Id,
                BrigadeId = employee.Brigade?.Id,
                EmployeeTypeId = employee.EmployeeType?.Id,
                PositionId = employee.Position?.Id
            };
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (existingEmployee == null)
                throw new Exception("Сотрудник не найден.");

            existingEmployee.FirstName = employee.FirstName ?? existingEmployee.FirstName;
            existingEmployee.LastName = employee.LastName ?? existingEmployee.LastName;
            existingEmployee.EmployeeTypeId = employee.EmployeeTypeId ?? existingEmployee.EmployeeTypeId;
            existingEmployee.PositionId = employee.PositionId ?? existingEmployee.PositionId;
            existingEmployee.ProjectId = employee.ProjectId ?? existingEmployee.ProjectId;
            existingEmployee.BrigadeId = employee.Brigade?.Id ?? existingEmployee.BrigadeId;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(int count = 2)
        {
            return await _context.Employees
                .Take(count)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ProjectId = e.ConstructionProject.Id,
                    BrigadeId = e.Brigade.Id,
                    EmployeeTypeId = e.EmployeeType.Id,
                    PositionId = e.Position.Id
                })
                .ToListAsync();
        }
    }
}