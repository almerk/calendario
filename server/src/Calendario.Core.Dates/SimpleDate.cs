using System;

namespace Calendario.Core.Dates
{
    public sealed record SimpleDate : Calendario.Core.Date
    {
        public DateTime Value { get; init; }
        public DateBelonging? Belonging { get; init; }
    }
}
