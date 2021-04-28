using Calendario.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreApp.DataConfiguration
{
    public class RelationConfiguration : IEntityTypeConfiguration<Relation>
    {
        public void Configure(EntityTypeBuilder<Relation> builder)
        {
            builder.ToTable("Relations");
            builder.HasOne(x => x.Object).WithMany(x => x.Relations).HasForeignKey("ObjectId");
            builder.HasOne(x => x.Subject).WithMany(x => x.Relations).HasForeignKey("SubjectId");
            builder.HasKey("ObjectId", "SubjectId");
            builder.ToTable("Relations");
            builder.OwnsOne(x => x.AccessRules);
        }
    }
}