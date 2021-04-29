using System;
namespace Calendario.Core.Dates
{
    public sealed record ReccurenceDate : Calendario.Core.Date
    {
        public DateTime Start { get; init; }
        public Reccurent.ReccurenceRule RRule { get; init; }

    }
}