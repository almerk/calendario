using System.Collections.Generic;
namespace Calendario.Core.Objects
{
    public class Event : Base.Object
    {
        public Calendar Calendar { get; init; }
        public Type Type => Calendar.Type;
        public virtual string Description { get; set; }
        public ICollection<Date> Dates { get; set; }
    }
}