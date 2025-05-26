using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    readonly IMapper _mapper;
    readonly ILeaveTypeRepository _leaveTypeRepository;
    readonly IAppLogger<Domain.LeaveType> _logger;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<Domain.LeaveType> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}"
                , nameof(Domain.LeaveType), request.Id);

            throw new BadRequestException("LeaveType is invalid!", validationResult);
        }

        Domain.LeaveType leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        return Unit.Value;
    }
}
