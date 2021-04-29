using System;

namespace Calendario.Core.Dates
{
    public sealed record ContinuousReccurenceDate: Calendario.Core.Date
    {
        public DateTime Start { get; init; }
        public DateTime End { get; }
        public Reccurent.ReccurenceRule RRule { get; init; }
    }
}