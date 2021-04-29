namespace Calendario.Core
{
    public abstract record Date : Base.ValueObject
    {
        public bool IsExcept { get; init; } = false;
        public bool HasTime { get; }
        public System.TimeZoneInfo TimeZone { get; }
    }
}