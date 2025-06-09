using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;


namespace ConstructionOrganizations;

public class AppDbContext : DbContext
{
    public DbSet<ConstructionOrganization> Organizations { get; set; } = null!;
    public DbSet<ConstructionDepartment> Departments { get; set; } = null!;
    public DbSet<ConstructionProject> Projects { get; set; } = null!;
    public DbSet<EmployeeType> EmployeeTypes { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<ObjectType> ObjectTypes { get; set; } = null!;
    public DbSet<ConstructionOrganizations.Models.Object> Objects { get; set; } = null!;
    public DbSet<Brigade> Brigades { get; set; } = null!;
    public DbSet<BrigadeMember> BrigadeMembers { get; set; } = null!;
    public DbSet<ObjectAttribute> ObjectAttributes { get; set; } = null!;
    public DbSet<WorkType> WorkTypes { get; set; } = null!;
    public DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;
    public DbSet<MaterialEstimate> MaterialEstimates { get; set; } = null!;
    public DbSet<MaterialUsage> MaterialUsages { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;
    public DbSet<EquipmentAssignment> EquipmentAssignments { get; set; } = null!;
    public DbSet<BrigadeWorkAssignment> BrigadeWorkAssignments { get; set; } = null!;
    public DbSet<EmployeeAssignment> EmployeeAssignments { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
