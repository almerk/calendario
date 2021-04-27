namespace Calendario.Core
{
    public record AccessRules
    {
        public bool CanRead { get; init; }
        public bool CanUpdate { get; init; }
        public bool CanDelete { get; init; }
        public bool CanCreateDependents { get; init; }
    }
}