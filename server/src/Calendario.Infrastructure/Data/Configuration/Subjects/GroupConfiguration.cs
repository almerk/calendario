using Calendario.Core.Subjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendario.Infrastructure.Data.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.HasOne(g => g.Parent).WithMany(g => g.Groups).OnDelete(DeleteBehavior.Cascade);
        }
    }
}