using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    readonly IMapper _mapper;
    readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        Domain.LeaveAllocation allocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.id)
            ?? throw new NotFoundException<Domain.LeaveAllocation>(request.id);
        return _mapper.Map<LeaveAllocationDetailsDto>(allocation);
    }
}
