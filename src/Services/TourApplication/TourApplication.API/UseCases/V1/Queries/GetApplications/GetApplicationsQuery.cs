using BuildingBlocks.Shared.ApiResult;
using MediatR;
using TourApplication.API.DTOs;

namespace TourApplication.API.UseCases.V1;

public record GetApplicationsQuery(Guid TourJobId) : IRequest<ApiResult<List<ApplicationDto>>>
{
}
