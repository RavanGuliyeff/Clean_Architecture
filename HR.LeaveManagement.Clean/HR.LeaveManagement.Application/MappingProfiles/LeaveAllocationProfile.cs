﻿using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>().ReverseMap();
        CreateMap<LeaveAllocation, CreateLeaveAllocationCommand>().ReverseMap();
        CreateMap<LeaveAllocation, UpdateLeaveAllocationCommand>().ReverseMap();
    }
}
