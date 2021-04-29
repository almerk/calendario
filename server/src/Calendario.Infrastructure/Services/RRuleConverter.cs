using System;
using System.Linq;
using Calendario.Core.Dates.Reccurent;
using AutoMapper;
using Ical.Net.DataTypes;

public sealed class RRuleConverter
{

    private static IMapper _mapper =
        new MapperConfiguration(conf =>
        {
            conf.CreateMap<ReccurenceRule, RecurrencePattern>()
            .ForMember(d => d.Until, opts => opts.MapFrom(s => s.Until))
            .ForMember(d => d.Count, opts => opts.MapFrom(s => s.Count ?? int.MinValue))
            .ForMember(d => d.ByDay, opts => opts.MapFrom(s => s.ByWeekDay
                    .Select(d => new Ical.Net.DataTypes.WeekDay((DayOfWeek)d))))
            .ForMember(d => d.FirstDayOfWeek, opt => opt.MapFrom(s => (DayOfWeek)s.FirstDayOfWeek))
            .ReverseMap()
            .ForMember(s => s.Count, opts => opts.MapFrom(d => (d.Count == int.MinValue ? null : (int?)d.Count)));
        }).CreateMapper();

    public static string Serialize(ReccurenceRule rrule)
    {
        var icalRRule = _mapper.Map<ReccurenceRule, RecurrencePattern>(rrule);
        return icalRRule.ToString();
    }

    public static ReccurenceRule Deserialize(string rruleStr)
    {
        var icalRRule = new RecurrencePattern(rruleStr);
        var rrule = _mapper.Map<RecurrencePattern, ReccurenceRule>(icalRRule);
        return rrule;
    }

}