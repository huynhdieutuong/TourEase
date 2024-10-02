using AutoMapper;
using BuildingBlocks.Messaging.Application;
using BuildingBlocks.Messaging.Enums;
using MassTransit;
using TourApplication.API.Models;
using TourApplication.API.Repositories.Interfaces;
using TourApplication.API.Services.Interfaces;

namespace TourApplication.API.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public ApplicationService(IApplicationRepository applicationRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task PublishTotalApplicantsUpdated(Guid tourJobId, ApplicationTypes type)
    {
        var totalApplicants = await _applicationRepository.CountTotalApplicantsAsync(tourJobId);
        await _publishEndpoint.Publish(new TotalApplicantsUpdated
        {
            TourJobId = tourJobId,
            TotalApplicants = totalApplicants,
            Type = type
        });
    }

    public async Task PublishApplicationAction(Application application, TourJob tourJob, ApplicationTypes type)
    {
        await _publishEndpoint.Publish(new ApplicationAction
        {
            Application = _mapper.Map<ApplicationMessage>(application),
            TourJob = _mapper.Map<TourJobMessage>(tourJob),
            Type = type
        });
    }
}
