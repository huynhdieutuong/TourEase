using BuildingBlocks.Contracts.Domains;
using Tour.Domain.Entities.Enums;

namespace Tour.Domain.Entities;
public class TourJob : EntityAuditBase<Guid>
{
    public string Title { get; set; }
    public int Days { get; set; }
    public decimal SalaryPerDay { get; set; }
    public Currency Currency { get; set; }
    public string? TourGuide { get; set; }
    public int? TotalApplicants { get; set; }
    public DateTimeOffset ExpiredDate { get; set; }
    public Status Status { get; set; }

    public TourDetail Detail { get; set; }

    public decimal Salary() => SalaryPerDay * Days;
}
