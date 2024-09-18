using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlocks.Contracts.Domains;
public abstract class MongoEntityAuditBase<T> : MongoEntityBase<T>
{
    [BsonElement("createdDate")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedDate")]
    public DateTime UpdatedDate { get; set; }

    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }

    [BsonElement("updatedBy")]
    public string? UpdatedBy { get; set; }

    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; } = false;

    [BsonElement("deletedDate")]
    public DateTime? DeletedDate { get; set; }
}
