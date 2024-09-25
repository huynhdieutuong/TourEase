using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tour.Application.DTOs;
using Tour.Application.UseCases.V1.Destinations;

namespace Tour.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class DestinationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DestinationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get Destinations
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResult<List<DestinationDto>>>> GetDestinations([FromQuery] string mode = "flat")
    {
        var query = new GetDestinationsQuery(mode);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get Detail Destination by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<DestinationDto>>> GetDestinationsById(Guid id)
    {
        var query = new GetDestinationByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Add Destination
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ApiResult<DestinationDto>>> CreateDestination(CreateDestinationCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDestinationsById), new { result.Data.Id }, result);
    }

    /// <summary>
    /// Update Destination
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResult<DestinationDto>>> UpdateDestination(Guid id, UpdateDestinationCommand command)
    {
        command.SetId(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete Destination by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDestinationsById(Guid id)
    {
        var command = new DeleteDestinationCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
