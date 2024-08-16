using BuildingBlocks.Contracts.Domains.Interfaces;

namespace BuildingBlocks.Contracts.Domains;
public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
{
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? DeletedDate { get; set; }
}
