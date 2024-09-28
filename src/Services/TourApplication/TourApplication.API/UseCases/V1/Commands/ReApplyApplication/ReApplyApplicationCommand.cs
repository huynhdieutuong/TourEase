using BuildingBlocks.Shared.ApiResult;
using MediatR;

namespace TourApplication.API.UseCases.V1;

public record ReApplyApplicationCommand(Guid ApplicationId, string Username) : IRequest<ApiResult<bool>>
{
}
