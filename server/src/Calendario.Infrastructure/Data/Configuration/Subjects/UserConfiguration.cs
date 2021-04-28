using Calendario.Core.Subjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendario.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasIndex(u => u.Login).IsUnique();
            builder.Property(u => u.Login).IsRequired();
            builder
           .HasOne(p => p.Group)
           .WithMany(t => t.Users)
           .OnDelete(DeleteBehavior.Cascade);
        }
    }
}