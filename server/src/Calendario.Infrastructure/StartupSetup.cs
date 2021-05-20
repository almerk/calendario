using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Calendario.Infrastructure.Data;
using Calendario.Infrastructure.Services;

namespace Calendario.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddAppDbContext(this IServiceCollection services, string connectionString) =>
           services.AddDbContext<AppDbContext>(opts =>
           {
               opts.UseNpgsql(connectionString);
           });
        public static void AddRepository(this IServiceCollection services) =>
            services.AddScoped<IRepository, EfRepository>();

        public static void AddAppConfiguration(this IServiceCollection services, Configuration configuration) => services.AddSingleton<Configuration>(configuration);
        public static void AddAppDbInitialSeed(this IServiceCollection services) => services.AddScoped<InitialDbSeed>();
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
            return new Configuration()
            {
                AdminLogin = env("CALENDARIO_ADMIN_LOGIN"),
                AdminName = env("CALENDARIO_ADMIN_NAME"),
                AdminPassword = env("CALENDARIO_ADMIN_PASSWORD")
            };
        }

        //TODO: Move Identity to separate service and configure provider
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("IdentityConnection"), sql =>
                {
                    sql.MigrationsAssembly(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name); 
                });
                options.LogTo(Console.WriteLine);
            });
            
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<IdentityContext>();
            services.ConfigureApplicationCookie(options =>
           {
                // Cookie settings
                options.Cookie.HttpOnly = true;
               options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

               options.LoginPath = "/Identity/Account/Login";
               options.AccessDeniedPath = "/Identity/Account/AccessDenied";
               options.SlidingExpiration = true;
           });
        }
    }
}