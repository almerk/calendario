using System;

namespace Calendario.Core.Dates
{
    public sealed record ContinuousDate : Calendario.Core.Date
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
    }
}
