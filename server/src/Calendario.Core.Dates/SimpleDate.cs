using System;

namespace Calendario.Core.Dates
{
    public record SimpleDate : Calendario.Core.Date
    {
        public DateTime Value { get; }
        public bool HasTime { get; }
        public TimeZoneInfo TimeZone { get; }
        public DateBelonging? Belonging { get; }
    }
}
