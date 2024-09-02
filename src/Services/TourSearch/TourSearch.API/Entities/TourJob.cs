using BuildingBlocks.Contracts.Domains;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TourSearch.API.Entities;

public class TourJob : MongoEntityAuditBase<Guid>
{
    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("slug")]
    public string Slug { get; set; }

    [BsonElement("days")]
    public int Days { get; set; }

    [BsonElement("salaryPerDay")]
    public decimal SalaryPerDay { get; set; }

    [BsonElement("salary")]
    public decimal Salary { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }

    [BsonElement("tourGuide")]
    public string? TourGuide { get; set; }

    [BsonElement("totalApplicants")]
    public int? TotalApplicants { get; set; }

    [BsonElement("expiredDate")]
    public DateTimeOffset ExpiredDate { get; set; }

    [BsonElement("status")]
    public string Status { get; set; }

    [BsonElement("itinerary")]
    public string Itinerary { get; set; }

    [BsonElement("imageUrl")]
    public string? ImageUrl { get; set; }

    [BsonElement("participants")]
    public int Participants { get; set; }

    [BsonElement("languageSpoken")]
    public string LanguageSpoken { get; set; }

    [BsonElement("startDate")]
    public DateTimeOffset StartDate { get; set; }

    [BsonElement("endDate")]
    public DateTimeOffset EndDate { get; set; }

    [BsonElement("destinationIds")]
    [BsonRepresentation(BsonType.String)]
    public List<Guid> DestinationIds { get; set; } = new List<Guid>();
}
