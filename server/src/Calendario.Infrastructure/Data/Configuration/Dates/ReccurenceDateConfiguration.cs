using Calendario.Core;
using Calendario.Core.Dates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreApp.DataConfiguration
{
    public class ReccurenceDateConfiguration : IEntityTypeConfiguration<ReccurenceDate>
    {
        public void Configure(EntityTypeBuilder<ReccurenceDate> builder)
        {

        }
    }
}