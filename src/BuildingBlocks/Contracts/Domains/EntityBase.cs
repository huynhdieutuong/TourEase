using BuildingBlocks.Contracts.Domains.Interfaces;

namespace BuildingBlocks.Contracts.Domains;
public abstract class EntityBase<T> : IEntityBase<T>
{
    public T Id { get; set; }
}
