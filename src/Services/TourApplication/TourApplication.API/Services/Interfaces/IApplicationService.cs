using BuildingBlocks.Messaging.Enums;
using TourApplication.API.Models;

namespace TourApplication.API.Services.Interfaces;

public interface IApplicationService
{
    Task PublishTotalApplicantsUpdated(Guid tourJobId, ApplicationTypes type);
    Task PublishApplicationAction(Application application, TourJob tourJob, ApplicationTypes type);
}
