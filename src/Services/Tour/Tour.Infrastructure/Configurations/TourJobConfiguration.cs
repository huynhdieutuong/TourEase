using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tour.Domain.Entities;
using Tour.Domain.Entities.Enums;

namespace Tour.Infrastructure.Configurations;
public class TourJobConfiguration : IEntityTypeConfiguration<TourJob>
{
    public void Configure(EntityTypeBuilder<TourJob> builder)
    {
        builder.Property(x => x.Status)
            .HasDefaultValue(Status.Live)
            .IsRequired();
    }
}
