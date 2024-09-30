using BuildingBlocks.Shared.ApiResult;
using MediatR;
using TourApplication.API.DTOs;

namespace TourApplication.API.UseCases.V1;

public class ApplyTourJobCommand : IRequest<ApiResult<ApplicationDto>>
{
    public Guid TourJobId { get; set; }
    public string Comment { get; set; }
    public string? Username { get; set; }
}
