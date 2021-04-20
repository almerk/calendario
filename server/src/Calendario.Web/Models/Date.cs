namespace Calendario.Web.Models
{
    public abstract class Date
    {
        public string EventId { get; set; }
        public string Type { get; set; }
        public bool IsExcept { get; set; }
    }
}