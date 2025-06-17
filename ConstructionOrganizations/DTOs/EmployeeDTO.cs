namespace ConstructionOrganizations.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? ProjectId { get; set; }
    public int? BrigadeId { get; set; }
    public int? EmployeeTypeId { get; set; }
    public int? PositionId { get; set; }
}