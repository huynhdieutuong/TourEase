using BuildingBlocks.Messaging.Application;
using MassTransit;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services.Interfaces;

namespace TourApplication.API.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public ApplicationService(IApplicationRepository applicationRepository, IPublishEndpoint publishEndpoint)
    {
        _applicationRepository = applicationRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishTotalApplicantsUpdated(Guid tourJobId)
    {
        var totalApplicants = await _applicationRepository.CountTotalApplicantsAsync(tourJobId);
        await _publishEndpoint.Publish(new TotalApplicantsUpdated
        {
            TourJobId = tourJobId,
            TotalApplicants = totalApplicants
        });
    }
}
