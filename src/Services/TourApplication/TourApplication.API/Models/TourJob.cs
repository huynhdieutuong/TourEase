﻿using BuildingBlocks.Contracts.Domains;

namespace TourApplication.API.Models;

public class TourJob : EntityBase<Guid>
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public DateTime ExpiredDate { get; set; }
    public string Owner { get; set; }
    public bool IsFinished { get; set; }
}
