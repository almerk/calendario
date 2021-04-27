namespace Calendario.Core
{
    public record Relation : Base.ValueObject
    {
        public Base.Subject Subject { get; init; }
        public Base.Object Object { get; init; }

        public AccessRules AccessRules { get; init; }
    }
}