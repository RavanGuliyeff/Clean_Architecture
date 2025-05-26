﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        readonly IMapper _mapper;
        readonly ILeaveAllocationRepository _leaveAllocationRepository;
        readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(IMapper mapper
            , ILeaveAllocationRepository leaveAllocationRepository
            , ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Leave Allocation is invalid", validationResult);
            }

            Domain.LeaveAllocation allocationToCreate = _mapper.Map<Domain.LeaveAllocation>(request);

            await _leaveAllocationRepository.CreateAsync(allocationToCreate);

            return allocationToCreate.Id;
        }
    }
}
