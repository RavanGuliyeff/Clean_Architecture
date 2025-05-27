using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    readonly IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveRequestController>
    [HttpGet("[action]")]
    public async Task<ActionResult<List<LeaveRequestDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetLeaveRequestsQuery()));
    }

    // GET api/<LeaveRequestController>/5
    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<LeaveRequestDetailsDto>> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetLeaveRequestDetailsQuery(id)));
    }

    // POST api/<LeaveRequestController>
    [HttpPost("[action]")]
    public async Task<ActionResult> Create([FromBody] CreateLeaveRequestCommand leaveRequest)
    {
        var response = await _mediator.Send(leaveRequest);
        return Ok(new { Id = response });
    }

    // PUT api/<LeaveRequestController>/5
    [HttpPut("[action]")]
    public async Task<ActionResult> Update([FromBody] UpdateLeaveRequestCommand leaveRequest)
    {
        await _mediator.Send(leaveRequest);
        return NoContent();
    }

    [HttpPut("[action]/{id}")]
    public async Task<ActionResult> Cancel([FromBody] int id)
    {
        await _mediator.Send(new CancelLeaveRequestCommand(id));
        return NoContent();
    }
    
    [HttpPut("[action]")]
    public async Task<ActionResult> ChangeApprovedStatus([FromBody] ChangeLeaveRequestApprovalCommand leaveRequest)
    {
        await _mediator.Send(leaveRequest);
        return NoContent();
    }


    // DELETE api/<LeaveRequestController>/5
    [HttpDelete("[action]/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveRequestCommand(id));
        return NoContent();
    }
}
