using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    readonly ILeaveTypeRepository _leaveTypeRepository;
    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(x => x.Id)
            .NotNull()
            .MustAsync(LeaveTypeExists);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} can not be empty!")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(x => x.DefaultDays)
            .NotEmpty().WithMessage("DefaultDays cannot be empty.")
            .GreaterThan(0).WithMessage("DefaultDays must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("DefaultDays must be less than or equal to 100.");
    }

    private async Task<bool> LeaveTypeExists(int id, CancellationToken token)
    {
        return await _leaveTypeRepository.GetByIdAsync(id) == null 
            ? false : true;
    }
}