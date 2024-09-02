using BuildingBlocks.Contracts.Domains.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlocks.Contracts.Domains;
public abstract class MongoEntityBase<T> : IEntityBase<T>
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.String)]
    public T Id { get; set; }
}
