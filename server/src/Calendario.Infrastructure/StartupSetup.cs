using Calendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace Calendario.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
           services.AddDbContext<AppDbContext>(opts =>
           {
               opts.UseNpgsql(connectionString);
           });
        public static string GetPostgresConnectionString(this IConfiguration configuration)
        {
            Func<string, string> errorMsg = s => $"Environment variable {s} must be specified.";
            return $@"Host={configuration["DB_HOST"] ?? "localhost"};
                      Port={configuration["DB_PORT"] ?? "5432"};
                      Database={configuration["POSTGRES_DB"] ?? throw new ArgumentNullException(errorMsg("POSTGRES_DB"))};
                      Username={configuration["POSTGRES_USER"] ?? throw new ArgumentNullException(errorMsg("POSTGRES_USER"))};
                      Password={configuration["POSTGRES_PASSWORD"] ?? throw new ArgumentNullException(errorMsg("POSTGRES_PASSWORD"))};";
        }
    }
}