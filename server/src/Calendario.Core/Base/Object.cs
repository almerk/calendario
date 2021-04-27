using System.Collections.Generic;
namespace Calendario.Core.Base
{
    public abstract class Object : Entity
    {
        public virtual string Name { get; set; }
        public ICollection<Relation> Relations { get; set; }
    }
}