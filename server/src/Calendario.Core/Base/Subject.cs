using System.Collections.Generic;
namespace Calendario.Core.Base
{
    public abstract class Subject : Entity
    {
        public ICollection<Relation> Relations { get; set; }
    }
}