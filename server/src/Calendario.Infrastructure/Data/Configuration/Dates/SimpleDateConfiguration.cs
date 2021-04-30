using Calendario.Core;
using Calendario.Core.Dates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendario.Infrastructure.Data.Configuration
{
    public class SimpleDateConfiguration : IEntityTypeConfiguration<SimpleDate>
    {
        public void Configure(EntityTypeBuilder<SimpleDate> builder)
        {
            
            //builder.WithOwner
        }
    }
}