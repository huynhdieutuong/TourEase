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
        // One TourGuide only apply TourJob once time
        // Only TourGuide role can Apply
        // In case user have 2 role (TourGuide and TravelAgency), need check Owner can't apply
        // Can't apply for a expired or finished job
        // Default Status of Application is Pending

        // Publish event to Tour and TourSearch to update TotalApplicants (only count <> Cancel)
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
        // Ony the TourGuide can cancel their Applications
        // Only Pending Status can change to Cancel
        // need check TourJob Status is complete or not

        // Publish event to Tour and TourSearch to update TotalApplicants
        var command = new CancelApplicationCommand(applicationId, User.Identity.Name);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{applicationId}/reapply")]
    [Authorize(Roles = Roles.TourGuide)]
    public async Task<ActionResult<ApiResult<bool>>> ReApplyApplication(Guid applicationId)
    {
        // Ony the TourGuide can re-apply their Applications
        // Only Cancel Status can change to Pending (ReApply)
        // need check TourJob Status is complete or not

        // Publish event to Tour and TourSearch to update TotalApplicants
        var command = new ReApplyApplicationCommand(applicationId, User.Identity.Name);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{applicationId}/choose")]
    [Authorize(Roles = Roles.TravelAgency)]
    public async Task<ActionResult<ApiResult<bool>>> ChooseTourGuide(Guid applicationId)
    {
        // Only the TravelAgency can choose TourGuide on their TourJobs
        // Only choose Pending Application
        // need check TourJob Status is complete or not
        // Update Status of Applications (All Status is Rejected, one is Accepted?)
        // Update TourGuide in TourJob
        var command = new ChooseTourGuideCommand(applicationId, User.Identity.Name);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    // Consume Delete TourJob, will delete all Applications as well
}
