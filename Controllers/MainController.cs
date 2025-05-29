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
            var org = new ConstructionOrganization { Name = "Главстрой" };
            _context.Organizations.Add(org);
            _context.SaveChanges();

            var dept = new ConstructionDepartment
            {
                Name = "Управление N1",
                OrganizationId = org.Id,
                Organization = org
            };
            _context.Departments.Add(dept);
            _context.SaveChanges();

            // 3. Добавляем проект
            var project = new ConstructionProject
            {
                Location = "Ленинский район",
                ManagementId = dept.Id,
                Department = dept
            };
            _context.Projects.Add(project);
            _context.SaveChanges();

            // 4. Добавляем типы сотрудников
            var workerType = new EmployeeType { TypeName = "Worker" };
            var itrType = new EmployeeType { TypeName = "ITR" };
            _context.EmployeeTypes.AddRange(workerType, itrType);
            _context.SaveChanges();

            // 5. Добавляем должности
            var position1 = new Position { Name = "Каменщик" };
            var position2 = new Position { Name = "Прораб" };
            var position3 = new Position { Name = "Бригадир" };
            _context.Positions.AddRange(position1, position2, position3);
            _context.SaveChanges();

            // 6. Добавляем сотрудников
            var employee1 = new Employee
            {
                FirstName = "Иван",
                LastName = "Иванов",
                EmployeeTypeId = workerType.Id,
                PositionId = position3.Id
            };

            var employee2 = new Employee
            {
                FirstName = "Петр",
                LastName = "Петров",
                EmployeeTypeId = workerType.Id,
                PositionId = position1.Id
            };

            _context.Employees.AddRange(employee1, employee2);
            _context.SaveChanges();

            // 7. Добавляем тип объекта
            var objectType = new ObjectType { TypeName = "Жилой дом" };
            _context.ObjectTypes.Add(objectType);
            _context.SaveChanges();

            // 8. Добавляем объект
            var obj = new ConstructionOrganizations.Models.Object
            {
                ObjectType = objectType.Id,
                ObjectTypeNavigation = objectType,
                ConstructionProjectId = project.Id,
                Project = project,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(90),
                Status = "In Progress"
            };
            _context.Objects.Add(obj);
            _context.SaveChanges();

            // 9. Добавляем характеристики объекта
            var attribute = new ObjectAttribute
            {
                ObjectId = obj.Id,
                AttributeName = "Этажность",
                AttributeValue = 9
            };
            _context.ObjectAttributes.Add(attribute);
            _context.SaveChanges();

            // 10. Добавляем бригаду
            var brigade = new Brigade();
            _context.Brigades.Add(brigade);
            _context.SaveChanges();

            // 11. Добавляем членов бригады
            var member = new BrigadeMember
            {
                BrigadeId = brigade.Id,
                EmployeeId = employee1.Id
            };
            _context.BrigadeMembers.Add(member);
            _context.SaveChanges();

            return Ok(new { Message = "Тестовые данные успешно добавлены!" });
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
