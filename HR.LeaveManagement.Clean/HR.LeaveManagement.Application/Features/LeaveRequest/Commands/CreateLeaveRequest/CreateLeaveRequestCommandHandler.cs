using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    readonly IMapper _mapper;
    readonly IEmailSender _emailSender;
    readonly ILeaveTypeRepository _leaveTypeRepository;
    readonly ILeaveRequestRepository _leaveRequestRepository;
    readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public CreateLeaveRequestCommandHandler(IMapper mapper
        , IEmailSender emailSender
        , ILeaveTypeRepository leaveTypeRepository
        , ILeaveRequestRepository leaveRequestRepository
        , IAppLogger<CreateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        CreateLeaveRequestCommandValidator validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);

        ValidationResult validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave request", validationResult);
        }

        Domain.LeaveRequest leaveRequestToCreate = _mapper.Map<Domain.LeaveRequest>(request);

        await _leaveRequestRepository.CreateAsync(leaveRequestToCreate);


        try
        {
            EmailMessage email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {leaveRequestToCreate.StartDate:D} to {leaveRequestToCreate.EndDate:D} has been submitted successfully!",
                Subject = "Leave request submitted"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return leaveRequestToCreate.Id;
    }
}
