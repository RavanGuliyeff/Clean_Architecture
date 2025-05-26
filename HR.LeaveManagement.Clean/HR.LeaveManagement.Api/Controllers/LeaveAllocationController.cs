using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationController : ControllerBase
{
    readonly IMediator _mediator;

    public LeaveAllocationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<LeaveAllocationDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetLeaveAllocationsQuery()));
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveAllocationDetailsQuery(id)));
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<int>> Create([FromBody] CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediator.Send(leaveAllocation);
        return Ok(new { Id = response });
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> Update([FromBody] UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveAllocationCommand(id));
        return NoContent();
    }
}
