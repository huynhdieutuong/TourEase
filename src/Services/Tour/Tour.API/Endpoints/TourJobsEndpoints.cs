using BuildingBlocks.Shared.ApiResult;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Tour.Application.DTOs;
using Tour.Application.UseCases.V1.TourJobs;

namespace Tour.API.Endpoints;

public class TourJobsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/tourjobs");

        group.MapGet("", GetTourJobs).WithName("GetTourJobs");
        group.MapGet("{id:Guid}", GetTourJobsById).WithName("GetTourJobsById");
        group.MapPost("", CreateTourJob).WithName("CreateTourJob");
        group.MapPut("{id:Guid}", UpdateTourJob).WithName("UpdateTourJob");
        group.MapDelete("{id:Guid}", DeleteTourJobsById).WithName("DeleteTourJobsById");
    }

    private async Task<Results<Ok<ApiResult<List<TourJobDto>>>, NotFound<string>>> GetTourJobs(IMediator mediator)
    {
        var userName = "test";
        var query = new GetTourJobsQuery(userName);
        var result = await mediator.Send(query);
        return TypedResults.Ok(result);
    }

    private async Task<Results<Ok<ApiResult<TourJobDto>>, NotFound<string>>> GetTourJobsById(Guid id, IMediator mediator)
    {
        var query = new GetTourJobByIdQuery(id);
        var result = await mediator.Send(query);
        return TypedResults.Ok(result);
    }

    private async Task<Results<Created<ApiResult<TourJobDto>>, NotFound<string>>> CreateTourJob(CreateTourJobCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return TypedResults.Created($"/api/tourjobs/{result.Data.Id}", result);
    }

    private async Task<Results<Ok<ApiResult<TourJobDto>>, NotFound<string>>> UpdateTourJob(Guid id, UpdateTourJobCommand command, IMediator mediator)
    {
        command.SetId(id);
        var result = await mediator.Send(command);
        return TypedResults.Ok(result);
    }

    private async Task<IResult> DeleteTourJobsById(Guid id, IMediator mediator)
    {
        var command = new DeleteTourJobCommand(id);
        await mediator.Send(command);
        return TypedResults.NoContent();
    }
}
