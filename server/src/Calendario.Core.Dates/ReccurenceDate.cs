using System;
namespace Calendario.Core.Dates
{
    public record ReccurenceDate : Calendario.Core.Date
    {
        public DateTime Start { get; }
        public bool HasTime { get; }
        public TimeZoneInfo TimeZone { get; }
        public Reccurent.ReccurenceRule RRule { get; }

    }
}