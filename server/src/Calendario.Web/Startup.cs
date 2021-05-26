using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SharedUtils.Configuration;
using Calendario.Infrastructure;
using Calendario.Infrastructure.Services.Account;

namespace Calendario.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                            .AddEnvFileConfiguration(configuration["EnvFilePath"])
                            .AddConfiguration(configuration)
                            .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentityServices(Configuration);

            services.AddAppDbContext(Configuration.GetPostgresConnectionString());
            services.AddAppConfiguration(Configuration.GetCalendarioConfiguration());

            services.AddScoped<RegisterUserService>();
            services.AddAppDbInitialSeedService();

            services.AddRepository();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendario.Web", Version = "v1" });
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendario.Web v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); //Required for identity pages temporary located in this project
                endpoints.MapControllers();
            });
        }
    }
}
