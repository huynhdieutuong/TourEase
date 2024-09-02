using BuildingBlocks.Contracts.Domains.Interfaces;
using MassTransit;

namespace BuildingBlocks.Contracts.Domains;
[ExcludeFromTopology]
public abstract class IntegrationEventEntityAuditBase<T> : IntegrationEventEntityBase<T>, IAuditable
{
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }
}
