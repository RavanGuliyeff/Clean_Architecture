using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    readonly IMapper _mapper;
    readonly ILeaveTypeRepository _leaveTypeRepository;
    readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.LeaveType> leaveTypes = await _leaveTypeRepository.GetAllAsync();
        
        List<LeaveTypeDto> data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        _logger.LogInformation("LeaveTypes retrieved successfully");

        return data;
    }
}
