﻿namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

public class BaseLeaveRequest
{
    public int LeaveTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
