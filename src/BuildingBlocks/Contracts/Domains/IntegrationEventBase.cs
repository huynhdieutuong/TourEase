using BuildingBlocks.Contracts.Domains.Interfaces;
using MassTransit;

namespace BuildingBlocks.Contracts.Domains;
[ExcludeFromTopology]
public abstract class IntegrationEventBase : IIntegrationEventBase
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTimeOffset CreationDate { get; } = DateTimeOffset.UtcNow;
}
