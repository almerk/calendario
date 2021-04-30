using System;
using System.Linq.Expressions;
using Calendario.Core;
using Calendario.Core.Dates;
using Calendario.Core.Dates.Reccurent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Calendario.Infrastructure.Data.Configuration
{
    public class ReccurenceDateConfiguration : IEntityTypeConfiguration<ReccurenceDate>, IEntityTypeConfiguration<ContinuousReccurenceDate>
    {

        private void ConfigureRRule<TDate>(EntityTypeBuilder<TDate> builder, Expression<Func<TDate, ReccurenceRule>> getRRuleProp)
        where TDate : Calendario.Core.Date
        {
            var rruleConversion = new ValueConverter<ReccurenceRule, string>(r => Services.RRule.StringConverter.Serialize(r), s => Services.RRule.StringConverter.Deserialize(s));
            builder.Property(getRRuleProp).HasConversion(rruleConversion);
        }

        public void Configure(EntityTypeBuilder<ReccurenceDate> builder)
        {
            ConfigureRRule(builder, x => x.RRule);
        }

        public void Configure(EntityTypeBuilder<ContinuousReccurenceDate> builder)
        {
            ConfigureRRule(builder, x => x.RRule);
        }
    }
}