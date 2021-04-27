#nullable enable
namespace Calendario.Core.Subjects
{
    public sealed class User : Base.Subject
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string Login { get; set; }
        public Group Group { get; set; }
    }
}