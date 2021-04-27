using System.Collections.Generic;
namespace Calendario.Core.Objects
{
    public class Calendar : Base.Object
    {
        public Type Type { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}