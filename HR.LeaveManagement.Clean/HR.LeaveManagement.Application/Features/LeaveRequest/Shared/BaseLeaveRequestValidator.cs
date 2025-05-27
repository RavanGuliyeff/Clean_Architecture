using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    readonly ILeaveTypeRepository _leaveTypeRepository;

    public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(e => e.StartDate)
            .LessThan(e => e.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(e => e.EndDate)
            .LessThan(e => e.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(e => e.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist);
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        return await _leaveTypeRepository.GetByIdAsync(id) != null;
    }
}
