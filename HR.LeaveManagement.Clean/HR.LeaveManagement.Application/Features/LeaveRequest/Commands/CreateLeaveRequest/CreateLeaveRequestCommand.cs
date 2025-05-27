﻿using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand :BaseLeaveRequest, IRequest<int>
{
    public string RequestComments { get; set; } = string.Empty;
}
