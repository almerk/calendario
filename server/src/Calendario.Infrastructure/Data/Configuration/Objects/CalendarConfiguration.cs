using Calendario.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreApp.DataConfiguration.Objects
{
    public class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
    {
        public void Configure(EntityTypeBuilder<Calendar> builder)
        {
            builder.ToTable("Calendars");
            builder
            .HasOne(x => x.Type)
            .WithMany(x => x.Calendars)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}