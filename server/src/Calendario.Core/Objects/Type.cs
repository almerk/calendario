using System.Collections.Generic;
namespace Calendario.Core.Objects
{
    public sealed class Type : Base.Object
    {
        public ICollection<Calendar> Calendars { get; set; }
    }
}