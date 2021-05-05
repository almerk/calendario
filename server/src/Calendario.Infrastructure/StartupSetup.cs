using Calendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Calendario.Infrastructure
{
    public static class StartupSetup
    {
         public static void AddDbContext(this IServiceCollection services) =>
            services.AddDbContext<AppDbContext>();
    }
}