using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.TourJobs;
public record GetTourJobsQuery(string UserName) : IRequest<ApiResult<List<TourJobDto>>>
{
}
