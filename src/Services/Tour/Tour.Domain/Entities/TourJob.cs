using BuildingBlocks.Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Tour.Domain.Entities.Enums;

namespace Tour.Domain.Entities;

[Index(nameof(Slug), IsUnique = true)]
public class TourJob : EntityAuditBase<Guid>
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public int Days { get; set; }
    public decimal SalaryPerDay { get; set; }
    public Currency Currency { get; set; }
    public string? TourGuide { get; set; }
    public int? TotalApplicants { get; set; }
    public DateTimeOffset ExpiredDate { get; set; }
    public Status Status { get; set; }

    public TourDetail Detail { get; set; }

    public decimal Salary() => Currency == Currency.VND ? Math.Floor(SalaryPerDay * Days) : SalaryPerDay * Days;
}
