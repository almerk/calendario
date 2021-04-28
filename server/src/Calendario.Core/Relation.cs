namespace Calendario.Core
{
    public sealed record Relation : Base.ValueObject
    {
        public Base.Subject Subject { get; init; }
        public Base.Object Object { get; init; }

        public AccessPermissions AccessRules { get; init; }
    }
}