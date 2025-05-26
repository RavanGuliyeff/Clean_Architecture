using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailsQueryHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        Domain.LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(request.id);

        LeaveTypeDetailDto data = _mapper.Map<LeaveTypeDetailDto>(leaveType);

        return data;
    }
}



