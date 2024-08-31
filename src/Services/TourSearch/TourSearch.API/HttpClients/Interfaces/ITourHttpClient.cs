using TourSearch.API.Entities;

namespace TourSearch.API.HttpClients.Interfaces;

public interface ITourHttpClient
{
    Task<List<Destination>> GetDestinationsFromTourService();
    Task<List<TourJob>> GetTourJobsFromTourService();
}
