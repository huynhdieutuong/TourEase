namespace BuildingBlocks.Contracts.Domains.Interfaces;
public interface IIntegrationEventBase
{
    Guid EventId { get; }
    DateTimeOffset CreationDate { get; }
}
