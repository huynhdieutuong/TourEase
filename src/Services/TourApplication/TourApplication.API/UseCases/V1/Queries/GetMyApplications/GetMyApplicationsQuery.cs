using BuildingBlocks.Shared.ApiResult;
using MediatR;
using TourApplication.API.DTOs;

namespace TourApplication.API.UseCases.V1;

public record GetMyApplicationsQuery(string Username) : IRequest<ApiResult<List<ApplicationDto>>>
{
}
