using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    readonly ILeaveTypeRepository _leaveTypeRepository;
    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(c => c.LeaveTypeId)
            .NotNull()
            .GreaterThan(0)
            .MustAsync(LeaveTypeExists)
            .WithMessage("{PropertyName} does not exists!");
            
    }

    private async Task<bool> LeaveTypeExists(int id, CancellationToken cancellationToken)
    {
        return await _leaveTypeRepository.GetByIdAsync(id) != null;
    }
}