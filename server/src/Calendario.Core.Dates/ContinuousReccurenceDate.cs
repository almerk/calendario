using System;

namespace Calendario.Core.Dates
{
    public class ContinuousReccurenceDate
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public bool HasTime { get; }
        public TimeZoneInfo TimeZone { get; }
        public Reccurent.ReccurenceRule RRule { get; }
    }
}