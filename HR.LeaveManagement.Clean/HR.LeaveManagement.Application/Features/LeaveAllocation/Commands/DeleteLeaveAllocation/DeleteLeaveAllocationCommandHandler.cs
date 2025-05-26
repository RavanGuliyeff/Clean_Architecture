using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        Domain.LeaveAllocation allocationToDelete = await _leaveAllocationRepository.GetByIdAsync(request.id)
            ?? throw new NotFoundException<Domain.LeaveAllocation>(request.id);

        await _leaveAllocationRepository.DeleteAsync(allocationToDelete);

        return Unit.Value;
    }
}
