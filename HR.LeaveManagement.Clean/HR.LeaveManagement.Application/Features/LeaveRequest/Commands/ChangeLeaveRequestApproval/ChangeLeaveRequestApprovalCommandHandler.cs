using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    readonly IAppLogger<ChangeLeaveRequestApprovalCommand> _logger;
    readonly IEmailSender _emailSender;
    readonly ILeaveRequestRepository _leaveRequestRepository;

    public ChangeLeaveRequestApprovalCommandHandler(IAppLogger<ChangeLeaveRequestApprovalCommand> logger
        , IEmailSender emailSender
        , ILeaveRequestRepository leaveRequestRepository)
    {
        _logger = logger;
        _emailSender = emailSender;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        ChangeLeaveRequestApprovalCommandValidator validator = new ChangeLeaveRequestApprovalCommandValidator();
        ValidationResult validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Leave Request invalid!", validationResult);
        }

        Domain.LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException<Domain.LeaveRequest>(request.Id);

        leaveRequest.Approved = request.Approved;

        await _leaveRequestRepository.UpdateAsync(leaveRequest);


        try
        {
            EmailMessage email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} approved status has been changed successfully!",
                Subject = "Leave request updated"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        throw new NotImplementedException();
    }
}
