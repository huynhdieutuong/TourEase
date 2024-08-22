using BuildingBlocks.Shared.ApiResult;
using MediatR;
using Tour.Application.DTOs;

namespace Tour.Application.UseCases.V1.TourJobs;
public class UpdateTourJobCommand : CreateOrUpdateCommand, IRequest<ApiResult<TourJobDto>>
{
    public Guid Id { get; set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
}
