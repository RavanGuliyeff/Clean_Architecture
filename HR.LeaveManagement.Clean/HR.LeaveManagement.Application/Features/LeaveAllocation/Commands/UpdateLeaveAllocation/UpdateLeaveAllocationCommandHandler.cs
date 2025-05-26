using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    readonly IMapper _mapper;
    readonly ILeaveAllocationRepository _leaveAllocationRepository;
    readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper
        , ILeaveAllocationRepository leaveAllocationRepository
        , ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Leave Allocation Invalid");
        }

        Domain.LeaveAllocation leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        _mapper.Map(request, leaveAllocation);

        await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }
}
