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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConstructionOrganization>().HasData(
            new ConstructionOrganization { Id = 1, Name = "Организация Тест" }
        );

        modelBuilder.Entity<ConstructionDepartment>().HasData(
            new ConstructionDepartment { Id = 1, OrganizationId = 1, Name = "Департамент А" }
        );

        modelBuilder.Entity<ConstructionProject>().HasData(
            new ConstructionProject { Id = 1, Location = "Новосибирск", DepartmentId = 1 }
        );

        modelBuilder.Entity<EmployeeType>().HasData(
            new EmployeeType { Id = 1, TypeName = "Рабочий" },
            new EmployeeType { Id = 2, TypeName = "Инженер" },
            new EmployeeType { Id = 3, TypeName = "Администратор" }
        );

        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Name = "Директор" },
            new Position { Id = 2, Name = "Менеджер" },
            new Position { Id = 3, Name = "Инженер ПИР" }
        );

        modelBuilder.Entity<ObjectType>().HasData(
            new ObjectType { Id = 1, TypeName = "Жилое здание" },
            new ObjectType { Id = 2, TypeName = "Коммерческое помещение" }
        );

        modelBuilder.Entity<WorkType>().HasData(
            new WorkType { Id = 1, Name = "Фундаментные работы" },
            new WorkType { Id = 2, Name = "Монтаж кровли" }
        );
    }
}
