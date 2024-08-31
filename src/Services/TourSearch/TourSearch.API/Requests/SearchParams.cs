using BuildingBlocks.Shared.Paging;

namespace TourSearch.API.Requests;

public class SearchParams : PagingParams
{
    public string? SearchTerm { get; set; }
    public string? OrderBy { get; set; }
    public string? Filter { get; set; }
}
