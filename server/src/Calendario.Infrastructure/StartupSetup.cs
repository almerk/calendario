using Calendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using Calendario.Infrastructure.Services;

namespace Calendario.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
           services.AddDbContext<AppDbContext>(opts =>
           {
               opts.UseNpgsql(connectionString);
           });
        public static void AddRepository(this IServiceCollection services) =>
            services.AddScoped<IRepository, EfRepository>();
        
        public static void AddAppConfiguration(this IServiceCollection services, Configuration configuration) => services.AddSingleton<Configuration>(configuration);
        public static void AddDbInitialSeed(this IServiceCollection services) => services.AddScoped<InitialDbSeed>();
        public static string GetPostgresConnectionString(this IConfiguration configuration)
        {
            Func<string, string> errorMsg = s => $"Environment variable {s} must be specified.";
            return $@"Host={configuration["DB_HOST"] ?? "localhost"};
                      Port={configuration["DB_PORT"] ?? "5432"};
                      Database={configuration["POSTGRES_DB"] ?? throw new ArgumentNullException(errorMsg("POSTGRES_DB"))};
                      Username={configuration["POSTGRES_USER"] ?? throw new ArgumentNullException(errorMsg("POSTGRES_USER"))};
                      Password={configuration["POSTGRES_PASSWORD"] ?? throw new ArgumentNullException(errorMsg("POSTGRES_PASSWORD"))};";
        }

        public static Configuration GetCalendarioConfiguration(this IConfiguration configuration)
        {
            Func<string, string> env = s => configuration[s] ?? throw new ArgumentNullException($"Environment variable {s} must be specified.");
            return new Configuration() {
                AdminLogin = env("CALENDARIO_ADMIN_LOGIN"),
                AdminName = env("CALENDARIO_ADMIN_NAME"),
                AdminPassword = env("CALENDARIO_ADMIN_PASSWORD")
            };
        }
    }
}