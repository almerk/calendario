using System;
namespace Calendario.Web.Models.Dates
{
    public class SimpleDate : Date
    {
        public DateTime DateTime { get; set; }
        public bool HasTime { get; set; }
    }
}