using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.Destinations;
public class CreateDestinationCommand : CreateOrUpdateDestinationCommand, IRequest<ApiResult<DestinationDto>>
{
}
