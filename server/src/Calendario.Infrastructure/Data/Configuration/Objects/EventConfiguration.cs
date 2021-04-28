using Calendario.Core.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreApp.DataConfiguration.Objects
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder
            .HasOne(x => x.Calendar)
            .WithMany(y => y.Events)
            .OnDelete(DeleteBehavior.Cascade);
            builder.OwnsMany(x => x.Dates);
        }
    }
}