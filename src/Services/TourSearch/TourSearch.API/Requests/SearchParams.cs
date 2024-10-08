﻿using BuildingBlocks.Shared.Paging;

namespace TourSearch.API.Requests;

public class SearchParams : PagingParams
{
    public string? SearchTerm { get; set; }
    public string? OrderBy { get; set; }
    public string? DestinationIds { get; set; }
    public string? Duration { get; set; }
    public string? Currency { get; set; }
    public bool IncludeFinished { get; set; } = false;
}
