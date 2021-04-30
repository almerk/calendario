using Calendario.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Calendario.Core;

namespace Calendario.Infrastructure.Data.Configuration
{
    public class DateConfiguration : IEntityTypeConfiguration<Date>
    {
        public void Configure(EntityTypeBuilder<Date> builder)
        {
            builder.ToTable("EventDates");

            builder.HasOne<Event>().WithMany(x => x.Dates).HasForeignKey("EventId");
            builder.HasDiscriminator<string>("Type");
            builder.Property<string>("Id").ValueGeneratedOnAdd();

            builder.Property(x => x.TimeZone).HasConversion(
                v => v.Id,
                id => TimeZoneInfo.FindSystemTimeZoneById(id)
            );
            builder.HasKey("EventId", "Type", "Id");
        }
    }
}