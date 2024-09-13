using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tour.Application.DTOs;
using Tour.Application.UseCases.V1.TourJobs;

namespace Tour.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.TravelAgency)]
public class TourJobsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TourJobsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get Tour Jobs By Logged in User
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResult<List<TourJobDto>>>> GetTourJobs()
    {
        var userName = User.Identity.Name ?? "tuongadmin";
        var query = new GetTourJobsQuery(userName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get Detail Tour Job by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<TourJobDto>>> GetTourJobsById(Guid id)
    {
        var query = new GetTourJobByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Add Tour Job
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ApiResult<TourJobDto>>> CreateTourJob(CreateTourJobCommand command)
    {
        command.CreatedBy = User.Identity.Name;
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTourJobsById), new { result.Data.Id }, result);
    }

    /// <summary>
    /// Update Tour Job
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResult<TourJobDto>>> UpdateTourJob(Guid id, UpdateTourJobCommand command)
    {
        command.UpdatedBy = User.Identity.Name;
        command.SetId(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete Tour Job by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTourJobsById(Guid id)
    {
        var command = new DeleteTourJobCommand(id, User.Identity.Name);
        await _mediator.Send(command);
        return NoContent();
    }
}
