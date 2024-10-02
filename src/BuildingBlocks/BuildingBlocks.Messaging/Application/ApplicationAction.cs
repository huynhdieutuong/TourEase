using BuildingBlocks.Contracts.Domains;
using BuildingBlocks.Messaging.Enums;

namespace BuildingBlocks.Messaging.Application;
public class ApplicationAction : IntegrationEventBase
{
    public ApplicationMessage Application { get; set; }
    public TourJobMessage TourJob { get; set; }
    public ApplicationTypes Type { get; set; }
}