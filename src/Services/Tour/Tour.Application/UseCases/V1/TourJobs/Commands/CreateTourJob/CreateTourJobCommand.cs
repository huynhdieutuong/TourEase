using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.TourJobs;
public class CreateTourJobCommand : CreateOrUpdateCommand, IRequest<ApiResult<TourJobDto>>
{
}
