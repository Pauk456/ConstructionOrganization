namespace ConstructionOrganizations.Models;

public class BrigadeMember
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int BrigadeId { get; set; }
    public Brigade Brigade { get; set; }
}

public class EmployeeAssignment
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int ProjectId { get; set; }
    public ConstructionProject Project { get; set; }
}
    
public class BrigadeWorkAssignment
{
    public int BrigadeId { get; set; }

    public Brigade Brigade { get; set; }
    public int WorkScheduleId { get; set; }
    public WorkSchedule WorkSchedule { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime CompletedDate { get; set; }
}

public class EquipmentAssignment
{
    public int EquipmentId { get; set; }
    public Employee Employee{ get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime ReturnedDate { get; set; }
}
