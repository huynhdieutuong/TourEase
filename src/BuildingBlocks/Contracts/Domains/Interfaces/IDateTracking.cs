namespace BuildingBlocks.Contracts.Domains.Interfaces;
public interface IDateTracking
{
    DateTimeOffset CreatedDate { get; set; }
    DateTimeOffset UpdatedDate { get; set; }
}
