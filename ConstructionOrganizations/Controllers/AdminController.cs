using ConstructionOrganizations;
using ConstructionOrganizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;
using Object = ConstructionOrganizations.Models.Object;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public record class SqlQueryRequest(string query);

    [Authorize]
    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteQuery([FromBody] SqlQueryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.query))
            return BadRequest("SQL запрос не указан");

        try
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = request.query;
            command.CommandType = CommandType.Text;

            using var reader = await command.ExecuteReaderAsync();
            var result = new List<Dictionary<string, object>>();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var value = reader.GetValue(i);
                    row[columnName] = value;
                }
                result.Add(row);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
    [HttpGet("init")]
    public async Task<IActionResult> Init()
    {
        var employeeTypes = new List<EmployeeType>
        {
            new EmployeeType {  TypeName = "Инженер" },
            new EmployeeType { TypeName  = "Мастер" },
            new EmployeeType { TypeName = "Рабочий"},
        };

        var positions = new List<Position>
        {
            new Position { Name = "Начальник участка" },
            new Position { Name = "Бригадир" },
            new Position { Name  = "Прораб" },
            new Position { Name  = "Архитектор" }
        };

        var objectTypes = new List<ObjectType>
        {
            new ObjectType { TypeName = "Жилой дом" },
            new ObjectType { TypeName = "Офисное здание" },
            new ObjectType { TypeName = "Дорго" }
        };

        var workTypes = new List<WorkType>
        {
            new WorkType { Name = "Фундаментные работы" },
            new WorkType { Name = "Кирпичные работы" },
            new WorkType { Name = "Прокладка водоснабжения" },
            new WorkType { Name = "Электромонтаж" }
        };

        _context.EmployeeTypes.AddRange(employeeTypes);
        _context.Positions.AddRange(positions);
        _context.ObjectTypes.AddRange(objectTypes);
        _context.WorkTypes.AddRange(workTypes);
        _context.SaveChanges();

        var organizations = new List<ConstructionOrganization>
        {
            new ConstructionOrganization
            {
                Name = "Организация А",
                Departments = new List<ConstructionDepartment>
                {
                    new ConstructionDepartment
                    {
                        Name = "Отдел А",
                    }
                }
            },
            new ConstructionOrganization
            {
                Name = "Организация Б",
                Departments = new List<ConstructionDepartment>
                {
                    new ConstructionDepartment
                    {
                        Name = "Отдел Б"
                    }
                }
            }
        };

        _context.Organizations.AddRange(organizations);
        _context.SaveChanges();

        var departments = _context.Organizations.ToList();

        var projects = new List<ConstructionProject>
        {
            new ConstructionProject
            {
                Location = "Новосибирск 1",
                DepartmentId = departments[0].Id
            },
            new ConstructionProject
            {
                Location = "Новосибирск 2",
                DepartmentId = departments[1].Id
            }
        };

        _context.Projects.AddRange(projects);
        _context.SaveChanges();

        var employees = new List<Employee>
        {
            new Employee
            {
                FirstName = "Иван",
                LastName = "Иванов",
                EmployeeTypeId = employeeTypes[0].Id,
                PositionId = positions[0].Id,
                ProjectId = projects[0].Id
            },
            new Employee
            {
                FirstName = "Петр",
                LastName = "Петров",
                EmployeeTypeId = employeeTypes[2].Id,
                PositionId = positions[2].Id,
                ProjectId = projects[1].Id
            },
             new Employee
            {
                FirstName = "Никита",
                LastName = "Петров",
                EmployeeTypeId = employeeTypes[0].Id,
                PositionId = positions[3].Id,
                ProjectId = projects[1].Id
            }
        };

        _context.Employees.AddRange(employees);
        _context.SaveChanges();

        var brigades = new List<Brigade>
        {
            new Brigade(),
            new Brigade()
        };

        _context.Brigades.AddRange(brigades);
        _context.SaveChanges();

        employees[0].BrigadeId = brigades[0].Id;
        employees[1].BrigadeId = brigades[1].Id;
        _context.SaveChanges();

        var equipments = new List<Equipment>
        {
            new Equipment
            {
                ManagementId = departments[0].Id,
                EquipmentName = "Бульдозер",
                EquipmentCount = 2
            },
            new Equipment
            {
                ManagementId = departments[1].Id,
                EquipmentName = "Лопата",
                EquipmentCount = 10
            }
        };

        _context.Equipments.AddRange(equipments);
        _context.SaveChanges();

        var objects = new List<Object>
        {
            new Object
            {
                ConstructionProjectId = projects[0].Id,
                ObjectType = objectTypes[0].Id,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow.AddMonths(6),
                Status = "В процессе",
                Customer = "ООО Застройщик"
            },
            new Object
            {
                ConstructionProjectId = projects[1].Id,
                ObjectType = objectTypes[2].Id,
                StartDate = DateTime.UtcNow.AddDays(-10),
                Status = "Начало работ",
                Customer = "Администрация города"
            }
        };

        _context.Objects.AddRange(objects);
        _context.SaveChanges();

        objects[0].EquipmentObjectAssignments.Add(new EquipmentObjectAssignment
        {
            EquipmentId = equipments[1].Id,
            ObjectId = objects[0].Id,
            count = 2
        });

        _context.SaveChanges();

        var attributes = new List<ObjectAttribute>
        {
            new ObjectAttribute
            {
                ObjectId = objects[0].Id,
                AttributeName = "Площадь",
                AttributeValue = 5000
            },
            new ObjectAttribute
            {
                ObjectId = objects[1].Id,
                AttributeName = "Протяжённость",
                AttributeValue = 12000
            }
        };

        _context.ObjectAttributes.AddRange(attributes);
        _context.SaveChanges();

        var workSchedules = new List<WorkSchedule>
        {
            new WorkSchedule
            {
                ObjectId = objects[0].Id,
                WorkTypeId = workTypes[0].Id,
                PlannedStartDate = DateTime.UtcNow.AddDays(-5),
                PlannedEndDate = DateTime.UtcNow.AddDays(10),
                ActualStartDate = DateTime.UtcNow.AddDays(-3)
            },
            new WorkSchedule
            {
                ObjectId = objects[1].Id,
                WorkTypeId = workTypes[2].Id,
                PlannedStartDate = DateTime.UtcNow.AddDays(5),
                PlannedEndDate = DateTime.UtcNow.AddDays(20)
            }
        };

        _context.WorkSchedules.AddRange(workSchedules);
        _context.SaveChanges();

        workSchedules[0].BrigadeWorkAssignments.Add(new BrigadeWorkAssignment
        {
            BrigadeId = brigades[0].Id,
            WorkScheduleId = workSchedules[0].Id
        });

        _context.SaveChanges();

        var materialEstimates = new List<MaterialEstimate>
        {
            new MaterialEstimate
            {
                ObjectId = objects[0].Id,
                MaterialName = "Бетон",
                MaterialCount = 200
            }
        };

        var materialUsages = new List<MaterialUsage>
        {
            new MaterialUsage
            {
                ObjectId = objects[0].Id,
                WorkScheduleId = workSchedules[0].Id,
                MaterialName = "Кирпич",
                MaterialCount = 180
            }
        };

        _context.MaterialEstimates.AddRange(materialEstimates);
        _context.MaterialUsages.AddRange(materialUsages);
        _context.SaveChanges();

      

        return Ok("База данных успешно заполнена тестовыми данными.");
    }
}