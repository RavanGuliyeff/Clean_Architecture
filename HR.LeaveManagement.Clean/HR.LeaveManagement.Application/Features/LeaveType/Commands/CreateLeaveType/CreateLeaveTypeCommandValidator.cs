using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;


        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} can not be empty!")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(x => x.DefaultDays)
            .NotEmpty().WithMessage("DefaultDays cannot be empty.")
            .GreaterThan(0).WithMessage("DefaultDays must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("DefaultDays must be less than or equal to 100.");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Name should be unique!");
    }

    private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUniqueAsync(command.Name);
    }


}