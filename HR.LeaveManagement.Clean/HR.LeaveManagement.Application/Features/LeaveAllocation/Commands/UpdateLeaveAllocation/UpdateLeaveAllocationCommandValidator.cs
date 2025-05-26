using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    readonly ILeaveTypeRepository _leaveTypeRepository;
    readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(e => e.NumberOfDays)
            .GreaterThan(0);

        RuleFor(e => e.Period)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(e => e.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeExists);

        RuleFor(e => e.Id)
            .GreaterThan(0)
            .MustAsync(LeaveAllocationExists);
    }

    private async Task<bool> LeaveTypeExists(int id, CancellationToken cancellationToken)
    {
        return await _leaveTypeRepository.GetByIdAsync(id) != null;
    }

    private async Task<bool> LeaveAllocationExists(int id, CancellationToken cancellationToken)
    {
        return await _leaveAllocationRepository.GetByIdAsync(id) != null;
    }
}
