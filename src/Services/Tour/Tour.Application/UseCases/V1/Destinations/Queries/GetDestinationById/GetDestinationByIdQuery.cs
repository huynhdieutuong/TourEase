using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.Destinations;
public record GetDestinationByIdQuery(Guid Id) : IRequest<ApiResult<DestinationDto>>
{
}
