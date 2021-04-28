using Calendario.Core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendario.Infrastructure.Data.Configuration
{
    public class ObjectConfiguration : IEntityTypeConfiguration<Object>
    {
        public void Configure(EntityTypeBuilder<Object> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.ToTable("Objects");
        }
    }
}