using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
{
    readonly IMapper _mapper;
    readonly ILeaveRequestRepository _leaveRequestRepository;

    public GetLeaveRequestsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        List<Domain.LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();

        return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
    }
}
