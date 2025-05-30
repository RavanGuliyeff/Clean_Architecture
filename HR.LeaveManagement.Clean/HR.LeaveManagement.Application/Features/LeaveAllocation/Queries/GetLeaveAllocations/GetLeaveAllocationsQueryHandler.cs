﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
{
    readonly IMapper _mapper;
    readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public GetLeaveAllocationsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {

        List<Domain.LeaveAllocation> leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();

        return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
    }
}
