﻿namespace Tour.Application.DTOs;
public class TourJobDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public int Days { get; set; }
    public decimal SalaryPerDay { get; set; }
    public decimal Salary { get; set; }
    public string Currency { get; set; }
    public string? TourGuide { get; set; }
    public int? TotalApplicants { get; set; }
    public DateTimeOffset ExpiredDate { get; set; }
    public string Status { get; set; }

    public string Itinerary { get; set; }
    public string? ImageUrl { get; set; }
    public int Participants { get; set; }
    public string LanguageSpoken { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public List<Guid> DestinationIds { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }

    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? DeletedDate { get; set; }
}
