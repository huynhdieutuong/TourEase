using BuildingBlocks.Shared.ApiResult;
using MediatR;

namespace TourApplication.API.UseCases.V1;

public record ChooseTourGuideCommand(Guid ApplicationId, string Owner) : IRequest<ApiResult<bool>>
{
}
