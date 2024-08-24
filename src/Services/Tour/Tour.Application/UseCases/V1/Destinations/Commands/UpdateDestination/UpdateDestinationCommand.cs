using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.Destinations;
public class UpdateDestinationCommand : CreateOrUpdateDestinationCommand, IRequest<ApiResult<DestinationDto>>
{
    public Guid Id { get; set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
}
