using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public record LeaveAllocationDto
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }

    public LeaveTypeDto LeaveType { get; set; }
    public int LeaveTypeId { get; set; }

    public int Period { get; set; }

}
