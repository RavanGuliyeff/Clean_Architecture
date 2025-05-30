﻿namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record LeaveTypeDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }

    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}
