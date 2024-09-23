using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.Destinations;
public record GetDestinationsQuery(string Mode) : IRequest<ApiResult<List<DestinationDto>>>
{
}
