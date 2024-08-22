using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.TourJobs;
public record GetTourJobByIdQuery(Guid Id) : IRequest<ApiResult<TourJobDto>>
{
}
