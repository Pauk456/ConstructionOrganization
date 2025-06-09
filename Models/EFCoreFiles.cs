using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstructionOrganizations.Models;

public class ConstructionOrganization
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
}

public class ConstructionDepartment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int OrganizationId { get; set; }
    public Models.ConstructionOrganization Organization { get; set; } = null!;
    public ICollection<ConstructionProject> Projects { get; set; } = new List<ConstructionProject>();
}

public class ConstructionProject
{
    public int Id { get; set; }
    public string Location { get; set; } = null!;
    public int ManagementId { get; set; }
    public ConstructionDepartment Department { get; set; } = null!;

    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();
    public ICollection<Object> Objects { get; set; } = new List<Object>();
}
public class EmployeeType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

public class Position
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int EmployeeTypeId { get; set; }
    public EmployeeType EmployeeType { get; set; } = null!;
    public int PositionId { get; set; }
    public Position Position { get; set; } = null!;

    public ICollection<BrigadeMember> BrigadeMemberships { get; set; } = new List<BrigadeMember>();
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();
}

public class ObjectType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = null!;

    public ICollection<Object> Objects { get; set; } = new List<Object>();
}

public class Object
{
    public int Id { get; set; }
    public int ObjectType { get; set; }
    public ObjectType ObjectTypeNavigation { get; set; } = null!;
    public int ConstructionProjectId { get; set; }
    public ConstructionProject Project { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = "In Progress";

    public ICollection<ObjectAttribute> Attributes { get; set; } = new List<ObjectAttribute>();
    public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
    public ICollection<MaterialEstimate> MaterialEstimates { get; set; } = new List<MaterialEstimate>();
    public ICollection<MaterialUsage> MaterialUsages { get; set; } = new List<MaterialUsage>();
    public ICollection<EquipmentAssignment> EquipmentAssignments { get; set; } = new List<EquipmentAssignment>();
}

public class Brigade
{
    public int Id { get; set; }

    public ICollection<BrigadeMember> Members { get; set; } = new List<BrigadeMember>();
    public ICollection<BrigadeWorkAssignment> Assignments { get; set; } = new List<BrigadeWorkAssignment>();
}

public class BrigadeMember
{
    public int Id { get; set; }
    public int BrigadeId { get; set; }
    public Brigade Brigade { get; set; } = null!;
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
}

public class ObjectAttribute
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; } = null!;
    public string AttributeName { get; set; } = null!;
    public long AttributeValue { get; set; }
}

public class WorkType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
}

public class WorkSchedule
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; } = null!;
    public int WorkTypeId { get; set; }
    public WorkType WorkType { get; set; } = null!;
    public DateTime PlannedStartDate { get; set; }
    public DateTime PlannedEndDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }

    public ICollection<MaterialUsage> MaterialUsages { get; set; } = new List<MaterialUsage>();
    public ICollection<BrigadeWorkAssignment> BrigadeAssignments { get; set; } = new List<BrigadeWorkAssignment>();
}

public class MaterialEstimate
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; } = null!;
    public string MaterialName { get; set; } = null!;
    public long MaterialCount { get; set; }
}

public class MaterialUsage
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; } = null!;
    public int WorkScheduleId { get; set; }
    public WorkSchedule WorkSchedule { get; set; } = null!;
    public string MaterialName { get; set; } = null!;
    public long MaterialCount { get; set; }
}

public class Equipment
{
    public int Id { get; set; }
    public int ManagementId { get; set; }
    public ConstructionDepartment Department { get; set; } = null!;
    public string EquipmentName { get; set; } = null!;
    public int EquipmentCount { get; set; }

    public ICollection<EquipmentAssignment> Assignments { get; set; } = new List<EquipmentAssignment>();
}

public class EquipmentAssignment
{
    public int Id { get; set; }
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;
    public int ObjectId { get; set; }
    public Object Object { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}

public class BrigadeWorkAssignment
{
    public int Id { get; set; }
    public int BrigadeId { get; set; }
    public Brigade Brigade { get; set; } = null!;
    public int WorkScheduleId { get; set; }
    public WorkSchedule WorkSchedule { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
}

public class EmployeeAssignment
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int ProjectId { get; set; }
    public ConstructionProject Project { get; set; } = null!;
}
