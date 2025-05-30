﻿using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public record LeaveRequestDetailsDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveTypeDto? LeaveType { get; set; } = null!;
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public DateTime DateActioned { get; set; }
    public string RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;
}
