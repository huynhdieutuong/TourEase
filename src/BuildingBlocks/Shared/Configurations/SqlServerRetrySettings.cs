using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Shared.Configurations;
public class SqlServerRetrySettings
{
    [Required, Range(5, 20)]
    public int MaxRetryCount { get; set; }

    [Required, Timestamp]
    public TimeSpan MaxRetryDelay { get; set; }

    public int[]? ErrorNumbersToAdd { get; set; }
}
