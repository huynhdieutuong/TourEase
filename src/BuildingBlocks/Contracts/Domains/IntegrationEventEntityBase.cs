using BuildingBlocks.Contracts.Domains.Interfaces;
using MassTransit;

namespace BuildingBlocks.Contracts.Domains;
[ExcludeFromTopology]
public abstract class IntegrationEventEntityBase<T> : IntegrationEventBase, IEntityBase<T>
{
    public T Id { get; set; }
}
