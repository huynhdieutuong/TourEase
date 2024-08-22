namespace BuildingBlocks.Contracts.Services;
public interface ISlugService
{
    string GenerateTourJobSlug(string title);
    string GenerateDestinationSlug(string title);
}
