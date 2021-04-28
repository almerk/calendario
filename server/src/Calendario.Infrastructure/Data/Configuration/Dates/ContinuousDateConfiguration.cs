using Calendario.Core;
using Calendario.Core.Dates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreApp.DataConfiguration
{
    public class ContinuousDateConfiguration : IEntityTypeConfiguration<ContinuousDate>
    {
        public void Configure(EntityTypeBuilder<ContinuousDate> builder)
        {

        }
    }
}