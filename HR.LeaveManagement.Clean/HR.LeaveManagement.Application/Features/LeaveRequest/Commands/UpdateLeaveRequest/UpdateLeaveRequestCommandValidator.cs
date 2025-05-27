using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    readonly ILeaveTypeRepository _leaveTypeRepository;
    readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

        RuleFor(e => e.Id)
            .GreaterThan(0)
            .MustAsync(LeaveRequestExist);
    }

    private async Task<bool> LeaveRequestExist(int id, CancellationToken token)
    {
        return await _leaveRequestRepository.GetByIdAsync(id) != null;
    }
}
