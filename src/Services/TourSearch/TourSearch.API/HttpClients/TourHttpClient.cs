using BuildingBlocks.Shared.ApiResult;
using TourSearch.API.Entities;
using TourSearch.API.HttpClients.Interfaces;

namespace TourSearch.API.HttpClients;

public class TourHttpClient : ITourHttpClient
{
    private readonly HttpClient _client;

    public TourHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Destination>> GetDestinationsFromTourService()
    {
        var response = await _client.GetFromJsonAsync<ApiResult<List<Destination>>>("destinations");

        if (!response.IsSucceeded) throw new Exception("Get destinations failed");

        return response?.Data ?? [];
    }

    public async Task<List<TourJob>> GetTourJobsFromTourService()
    {
        var response = await _client.GetFromJsonAsync<ApiResult<List<TourJob>>>("tourjobs");

        if (!response.IsSucceeded) throw new Exception("Get tour jobs failed");

        return response?.Data ?? [];
    }
}
