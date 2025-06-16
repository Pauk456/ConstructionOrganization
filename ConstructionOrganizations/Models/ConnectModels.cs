using System.ComponentModel.DataAnnotations;

namespace ConstructionOrganizations.Models;
    
public class BrigadeWorkAssignment
{
    [Key]
    public int Id { get; set; }
    public int BrigadeId { get; set; }
    public Brigade Brigade { get; set; }
    public int WorkScheduleId { get; set; }
    public WorkSchedule WorkSchedule { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime CompletedDate { get; set; }
}

public class EquipmentObjectAssignment
{
    [Key]
    public int Id { get; set; }
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
    public int ObjectId { get; set; }
    public Object Object { get; set; }
    public int count { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime ReturnedDate { get; set; }
}
