#nullable enable
using System.Collections.Generic;
namespace Calendario.Core.Subjects
{
    public sealed class Group : Base.Subject
    {
        public string Name { get; set; }
        public Group? Parent { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}