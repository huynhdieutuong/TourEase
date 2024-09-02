using BuildingBlocks.Contracts.Domains.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlocks.Contracts.Domains;
public abstract class MongoEntityAuditBase<T> : MongoEntityBase<T>, IAuditable
{
    [BsonElement("createdDate")]
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    [BsonElement("updatedDate")]
    public DateTimeOffset UpdatedDate { get; set; }

    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }

    [BsonElement("updatedBy")]
    public string? UpdatedBy { get; set; }

    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; } = false;

    [BsonElement("deletedDate")]
    public DateTimeOffset? DeletedDate { get; set; }
}
