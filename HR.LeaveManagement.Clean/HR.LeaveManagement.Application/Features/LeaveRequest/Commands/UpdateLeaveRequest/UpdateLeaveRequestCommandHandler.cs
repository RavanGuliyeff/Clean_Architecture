using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    readonly IMapper _mapper;
    readonly IEmailSender _emailSender;
    readonly ILeaveRequestRepository _leaveRequestRepository;
    readonly ILeaveTypeRepository _leaveTypeRepository;
    readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(IMapper mapper
        , IEmailSender emailSender
        , ILeaveRequestRepository leaveRequestRepository
        , ILeaveTypeRepository leaveTypeRepository
        , IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        Domain.LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException<Domain.LeaveRequest>(request.Id);


        UpdateLeaveRequestCommandValidator validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);

        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave request", validationResult);
        }

        _mapper.Map(request, leaveRequest);

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        try
        {
            EmailMessage email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated succesfully!",
                Subject = "Leave request updated"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return Unit.Value;
    }
}
