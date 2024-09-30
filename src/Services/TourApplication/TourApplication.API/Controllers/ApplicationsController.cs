using BuildingBlocks.Shared.ApiResult;
using BuildingBlocks.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourApplication.API.DTOs;
using TourApplication.API.UseCases.V1;

namespace TourApplication.API.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<List<ApplicationDto>>>> GetApplications([FromQuery] Guid tourJobId)
    {
        var query = new GetApplicationsQuery(tourJobId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = Roles.TourGuide)]
    public async Task<ActionResult<ApiResult<ApplicationDto>>> ApplyTourJob(ApplyTourJobCommand command)
    {
        command.Username = User.Identity.Name;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("my")]
    [Authorize(Roles = Roles.TourGuide)]
    public async Task<ActionResult<ApiResult<List<ApplicationDto>>>> GetMyApplications()
    {
        var query = new GetMyApplicationsQuery(User.Identity.Name);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{applicationId}/cancel")]
    [Authorize(Roles = Roles.TourGuide)]
    public async Task<ActionResult<ApiResult<bool>>> CancelApplication(Guid applicationId)
    {
        var command = new CancelApplicationCommand(applicationId, User.Identity.Name);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{applicationId}/reapply")]
    [Authorize(Roles = Roles.TourGuide)]
    public async Task<ActionResult<ApiResult<bool>>> ReApplyApplication(Guid applicationId)
    {
        var command = new ReApplyApplicationCommand(applicationId, User.Identity.Name);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{applicationId}/choose")]
    [Authorize(Roles = Roles.TravelAgency)]
    public async Task<ActionResult<ApiResult<bool>>> ChooseTourGuide(Guid applicationId)
    {
        var command = new ChooseTourGuideCommand(applicationId, User.Identity.Name);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
