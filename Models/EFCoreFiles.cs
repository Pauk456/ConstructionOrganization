namespace ConstructionOrganizations.Models;

// Models/ConstructionOrganization.cs
public class ConstructionOrganization
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<ConstructionDepartment> Departments { get; set; } = new List<ConstructionDepartment>();
}

// Models/ConstructionDepartment.cs
public class ConstructionDepartment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int OrganizationId { get; set; }
    public ConstructionOrganization Organization { get; set; } = null!;
    public ICollection<ConstructionProject> Projects { get; set; } = new List<ConstructionProject>();
}

// Models/ConstructionProject.cs
public class ConstructionProject
{
    public int Id { get; set; }
    public string Location { get; set; } = null!;
    public int ManagementId { get; set; }
    public ConstructionDepartment Department { get; set; } = null!;

    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();
    public ICollection<Object> Objects { get; set; } = new List<Object>();
}

// Models/EmployeeType.cs
public class EmployeeType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

// Models/Position.cs
public class Position
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

// Models/Employee.cs
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

// Models/ObjectType.cs
public class ObjectType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = null!;

    public ICollection<Object> Objects { get; set; } = new List<Object>();
}

// Models/Object.cs
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

// Models/Brigade.cs
public class Brigade
{
    public int Id { get; set; }

    public ICollection<BrigadeMember> Members { get; set; } = new List<BrigadeMember>();
    public ICollection<BrigadeWorkAssignment> Assignments { get; set; } = new List<BrigadeWorkAssignment>();
}

// Models/BrigadeMember.cs
public class BrigadeMember
{
    public int Id { get; set; }
    public int BrigadeId { get; set; }
    public Brigade Brigade { get; set; } = null!;
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
}

// Models/ObjectAttribute.cs
public class ObjectAttribute
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; } = null!;
    public string AttributeName { get; set; } = null!;
    public long AttributeValue { get; set; }
}

// Models/WorkType.cs
public class WorkType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
}

// Models/WorkSchedule.cs
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

// Models/MaterialEstimate.cs
public class MaterialEstimate
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; } = null!;
    public string MaterialName { get; set; } = null!;
    public long MaterialCount { get; set; }
}

// Models/MaterialUsage.cs
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

// Models/Equipment.cs
public class Equipment
{
    public int Id { get; set; }
    public int ManagementId { get; set; }
    public ConstructionDepartment Department { get; set; } = null!;
    public string EquipmentName { get; set; } = null!;
    public int EquipmentCount { get; set; }

    public ICollection<EquipmentAssignment> Assignments { get; set; } = new List<EquipmentAssignment>();
}

// Models/EquipmentAssignment.cs
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

// Models/BrigadeWorkAssignment.cs
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

// Models/EmployeeAssignment.cs
public class EmployeeAssignment
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int ProjectId { get; set; }
    public ConstructionProject Project { get; set; } = null!;
}