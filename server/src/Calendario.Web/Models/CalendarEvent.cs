namespace Calendario.Web.Models
{
    public class CalendarEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Date[] Dates { get; set; }
    }
}