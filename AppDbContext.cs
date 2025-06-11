using ConstructionOrganizations.Models;
using Microsoft.EntityFrameworkCore;
using Object = ConstructionOrganizations.Models.Object;


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
    public DbSet<ObjectAttribute> ObjectAttributes { get; set; } = null!;
    public DbSet<WorkType> WorkTypes { get; set; } = null!;
    public DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;
    public DbSet<MaterialEstimate> MaterialEstimates { get; set; } = null!;
    public DbSet<MaterialUsage> MaterialUsages { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(b => b.Brigade)
            .WithMany(m => m.Members)
            .HasForeignKey(m => m.BrigadeId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.ConstructionProject)
            .WithMany(m => m.Employees)
            .HasForeignKey(m => m.ProjectId);

        modelBuilder.Entity<Brigade>()
            .HasMany(e => e.BrigadeWorkAssignments)
            .WithOne(m => m.Brigade)
            .HasForeignKey(m => m.BrigadeId);

        modelBuilder.Entity<WorkSchedule>()
            .HasMany(e => e.BrigadeWorkAssignments)
            .WithOne(m => m.WorkSchedule)
            .HasForeignKey(m => m.WorkScheduleId);

        modelBuilder.Entity<Equipment>()
            .HasMany(e => e.EquipmentObjectAssignments)
            .WithOne(m => m.Equipment)
            .HasForeignKey(m => m.EquipmentId);

        modelBuilder.Entity<Object>()
            .HasMany(e => e.EquipmentObjectAssignments)
            .WithOne(m => m.Object)
            .HasForeignKey(m => m.ObjectId);

        modelBuilder.Entity<ConstructionOrganization>()
           .HasMany(b => b.Departments)
           .WithOne(m => m.Organization)
           .HasForeignKey(m => m.OrganizationId);

        modelBuilder.Entity<ConstructionDepartment>()
           .HasMany(b => b.Projects)
           .WithOne(m => m.Department)
           .HasForeignKey(m => m.DepartmentId);


        // 

        modelBuilder.Entity<Object>()
            .HasMany(b => b.WorkSchedules)
            .WithOne(m => m.Object)
            .HasForeignKey(m => m.ObjectId);

        modelBuilder.Entity<Object>()
            .HasMany(b => b.MaterialEstimates)
            .WithOne(m => m.Object)
            .HasForeignKey(m => m.ObjectId);

        modelBuilder.Entity<Object>()
            .HasMany(b => b.MaterialUsages)
            .WithOne(m => m.Object)
            .HasForeignKey(m => m.ObjectId);

        modelBuilder.Entity<Object>()
            .HasMany(b => b.Attributes)
            .WithOne(m => m.Object)
            .HasForeignKey(m => m.ObjectId);

        modelBuilder.Entity<WorkSchedule>()
            .HasMany(b => b.MaterialUsages)
            .WithOne(m => m.WorkSchedule)
            .HasForeignKey(m => m.WorkScheduleId);

        modelBuilder.Entity<MaterialUsage>()
            .HasOne(b => b.Object)
            .WithMany(m => m.MaterialUsages)
            .HasForeignKey(m => m.ObjectId);


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
