using BuildingBlocks.Contracts.Domains;

namespace BuildingBlocks.Messaging.Application;
public class TourJobFinished : IntegrationEventBase
{
    public TourJobMessage TourJob { get; set; }
    public string TourGuide { get; set; }
}
