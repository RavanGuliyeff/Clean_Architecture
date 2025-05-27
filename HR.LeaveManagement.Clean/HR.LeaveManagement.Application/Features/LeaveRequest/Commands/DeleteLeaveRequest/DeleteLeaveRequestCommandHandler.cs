using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        Domain.LeaveRequest leaveRequestToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException<Domain.LeaveRequest>(request.Id);

        await _leaveRequestRepository.DeleteAsync(leaveRequestToDelete);

        return Unit.Value;
    }
}
