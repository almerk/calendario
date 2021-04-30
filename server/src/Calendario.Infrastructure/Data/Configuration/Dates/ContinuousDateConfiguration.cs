using Calendario.Core;
using Calendario.Core.Dates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendario.Infrastructure.Data.Configuration
{
    public class ContinuousDateConfiguration : IEntityTypeConfiguration<ContinuousDate>
    {
        public void Configure(EntityTypeBuilder<ContinuousDate> builder)
        {

        }
    }
}