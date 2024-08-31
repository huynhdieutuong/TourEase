using BuildingBlocks.Contracts.Domains;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TourSearch.API.Entities;

public class Destination : MongoEntityBase
{
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("slug")]
    public string Slug { get; set; }

    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("imageUrl")]
    public string? ImageUrl { get; set; }

    [BsonElement("parentId")]
    [BsonRepresentation(BsonType.String)]
    public Guid? ParentId { get; set; }
}
