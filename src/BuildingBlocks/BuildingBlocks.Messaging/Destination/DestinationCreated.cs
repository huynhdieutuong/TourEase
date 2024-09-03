using BuildingBlocks.Contracts.Domains;

namespace BuildingBlocks.Messaging.Destination;
public class DestinationCreated : IntegrationEventEntityBase<Guid>
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Type { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? ParentId { get; set; }
}
