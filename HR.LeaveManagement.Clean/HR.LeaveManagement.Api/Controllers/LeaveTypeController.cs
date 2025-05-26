using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypeController : ControllerBase
{
    readonly IMediator _mediator;

    public LeaveTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<LeaveTypeDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetLeaveTypesQuery()));
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<LeaveTypeDetailDto>> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveTypeDetailsQuery(id)));
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Create([FromBody] CreateLeaveTypeCommand leaveType)
    {
        var response = await _mediator.Send(leaveType);
        return Ok(new { Id = response });
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> Update([FromBody] UpdateLeaveTypeCommand leaveType)
    {
        await _mediator.Send(leaveType);
        return NoContent();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveTypeCommand { Id = id });
        return NoContent();
    }
}
