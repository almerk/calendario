using System;

namespace Calendario.Core.Dates
{
    public record ContinuousDate : Calendario.Core.Date
    {
        public ContinuousDate(DateTime start, DateTime end, bool hasTime, TimeZoneInfo timeZone = null)
        {
            Start = hasTime ? start : start.Date;
            End = hasTime ? end : end.Date;
            if (end < start)
                throw new ArgumentException("Start date could not be greater than end date");
            HasTime = hasTime;
            TimeZone = timeZone ?? TimeZoneInfo.Local;
        }

        public DateTime Start { get; }
        public DateTime End { get; }
        public bool HasTime { get; }
        public TimeZoneInfo TimeZone { get; }
    }
}
