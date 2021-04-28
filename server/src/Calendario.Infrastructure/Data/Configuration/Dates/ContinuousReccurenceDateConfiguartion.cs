using Calendario.Core;
using Calendario.Core.Dates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreApp.DataConfiguration
{
    public class ContinuousReccurenceDateConfiguration : IEntityTypeConfiguration<ContinuousReccurenceDate>
    {
        public void Configure(EntityTypeBuilder<ContinuousReccurenceDate> builder)
        {
            
        }
    }
}