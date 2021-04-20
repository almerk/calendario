using Microsoft.EntityFrameworkCore;
namespace Calendario.Web.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CalendarEvent>(eb =>
           {
               eb.OwnsMany<Models.Date>(x => x.Dates);
           });
            // builder.Entity<Models.Date>(eb =>
            // {
            //     eb.HasNoKey()
            //     .HasOne<CalendarEvent>()
            //     .WithMany(x => x.Dates)
            //     .HasForeignKey(x => x.EventId)
            //     .OnDelete(DeleteBehavior.Cascade);
            // });

        }
    }
}