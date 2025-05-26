namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public record LeaveTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
