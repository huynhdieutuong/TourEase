using BuildingBlocks.Messaging.Enums;

namespace TourApplication.API.Services.Interfaces;

public interface IApplicationService
{
    Task PublishTotalApplicantsUpdated(Guid tourJobId, ApplicationTypes type);
}
