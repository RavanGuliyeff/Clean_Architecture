using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public record GetLeaveAllocationDetailsQuery(int id) : IRequest<LeaveAllocationDetailsDto>;
