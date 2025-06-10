using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionOrganizations.Models;


[Table("ConstructionOrganizations")]
public class ConstructionOrganization
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [Column("Name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;
}

[Table("ConstructionDepartments")]
public class ConstructionDepartment
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("OrganizationId")]
    public int? OrganizationId { get; set; }

    [ForeignKey("OrganizationId")]
    public ConstructionOrganization? Organization { get; set; }

    public ICollection<ConstructionProject> Projects { get; set; } = new List<ConstructionProject>();
}

[Table("ConstructionProject")]
public class ConstructionProject
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Location")]
    [StringLength(255)]
    public string? Location { get; set; }

    [Column("DepartmentId")]
    public int? DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public ConstructionDepartment? Department { get; set; }
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();

}

[Table("EmployeeTypes")]
public class EmployeeType
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("TypeName")]
    [StringLength(50)]
    public string TypeName { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

[Table("Positions")]
public class Position
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

[Table("Employees")]
public class Employee
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("FirstName")]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column("LastName")]
    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Column("EmployeeTypeId")]
    public int? EmployeeTypeId { get; set; }

    [ForeignKey("EmployeeTypeId")]
    public EmployeeType? EmployeeType { get; set; }

    [Column("PositionId")]
    public int? PositionId { get; set; }

    [ForeignKey("PositionId")]
    public Position? Position { get; set; }

    public ICollection<BrigadeMember> BrigadeMemberships { get; set; } = new List<BrigadeMember>();
    public ICollection<EmployeeAssignment> EmployeeAssignments { get; set; } = new List<EmployeeAssignment>();
}

[Table("ObjectTypes")]
public class ObjectType
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("TypeName")]
    [StringLength(100)]
    public string TypeName { get; set; } = null!;

    public ICollection<Object> Objects { get; set; } = new List<Object>();
}

[Table("Objects")]
public class Object
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ConstructionProjectId")]
    public int? ConstructionProjectId { get; set; }

    [ForeignKey("ConstructionProjectId")]
    public ConstructionProject? Project { get; set; }

    [Column("ObjectType")]
    public int? ObjectType { get; set; }

    [ForeignKey("ObjectType")]
    public ObjectType? ObjectTypeNavigation { get; set; }

    [Column("StartDate")]
    public DateTime? StartDate { get; set; }

    [Column("EndDate")]
    public DateTime? EndDate { get; set; }

    [Column("Status")]
    [StringLength(50)]
    public string? Status { get; set; }

    public ICollection<ObjectAttribute> Attributes { get; set; } = new List<ObjectAttribute>();
    public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
    public ICollection<MaterialEstimate> MaterialEstimates { get; set; } = new List<MaterialEstimate>();
    public ICollection<MaterialUsage> MaterialUsages { get; set; } = new List<MaterialUsage>();
}

[Table("Brigades")]
public class Brigade
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public ICollection<BrigadeMember> Members { get; set; } = new List<BrigadeMember>();
}

[Table("ObjectAttributes")]
public class ObjectAttribute
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ObjectId")]
    public int? ObjectId { get; set; }

    [ForeignKey("ObjectId")]
    public Object? Object { get; set; }

    [Required]
    [Column("AttributeName")]
    [StringLength(100)]
    public string AttributeName { get; set; } = null!;

    [Required]
    [Column("AttributeValue")]
    public long AttributeValue { get; set; }
}

[Table("WorkTypes")]
public class WorkType
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
}

[Table("WorkSchedules")]
public class WorkSchedule
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ObjectId")]
    public int? ObjectId { get; set; }

    [ForeignKey("ObjectId")]
    public Object? Object { get; set; }

    [Column("WorkTypeId")]
    public int? WorkTypeId { get; set; }

    [ForeignKey("WorkTypeId")]
    public WorkType? WorkType { get; set; }

    [Required]
    [Column("PlannedStartDate")]
    public DateTime PlannedStartDate { get; set; }

    [Required]
    [Column("PlannedEndDate")]
    public DateTime PlannedEndDate { get; set; }

    [Column("ActualStartDate")]
    public DateTime? ActualStartDate { get; set; }

    [Column("ActualEndDate")]
    public DateTime? ActualEndDate { get; set; }

    public ICollection<MaterialUsage> MaterialUsages { get; set; } = new List<MaterialUsage>();
}

[Table("MaterialEstimates")]
public class MaterialEstimate
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ObjectId")]
    public int? ObjectId { get; set; }

    [ForeignKey("ObjectId")]
    public Object? Object { get; set; }

    [Required]
    [Column("MaterialName")]
    [StringLength(100)]
    public string MaterialName { get; set; } = null!;

    [Required]
    [Column("MaterialCount")]
    public long MaterialCount { get; set; }
}

[Table("MaterialUsages")]
public class MaterialUsage
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ObjectId")]
    public int? ObjectId { get; set; }

    [ForeignKey("ObjectId")]
    public Object? Object { get; set; }

    [Column("WorkScheduleId")]
    public int? WorkScheduleId { get; set; }

    [ForeignKey("WorkScheduleId")]
    public WorkSchedule? WorkSchedule { get; set; }

    [Required]
    [Column("MaterialName")]
    [StringLength(100)]
    public string MaterialName { get; set; } = null!;

    [Required]
    [Column("MaterialCount")]
    public long MaterialCount { get; set; }
}

[Table("Equipments")]
public class Equipment
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ManagementId")]
    public int? ManagementId { get; set; }

    [ForeignKey("ManagementId")]
    public ConstructionDepartment? Department { get; set; }

    [Required]
    [Column("EquipmentName")]
    [StringLength(100)]
    public string EquipmentName { get; set; } = null!;

    [Required]
    [Column("EquipmentCount")]
    public int EquipmentCount { get; set; }
}
