using System;
using System.Linq;
using Calendario.Core.Subjects;
using Calendario.Infrastructure.Data;

namespace Calendario.Infrastructure.Services
{
    public class InitialDbSeed
    {
        private Configuration _configuration;

        public InitialDbSeed(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void SeedDbContext(AppDbContext context)
        {
            if(IsContextAlreadySeed(context)) return;

            var rootGroup = new Group() {
                Name = "All"
            };
            context.Add(rootGroup);
            var adminUser = new User() {
                Name = _configuration.AdminName,
                Login = _configuration.AdminLogin,
                Group = rootGroup
            };
            context.Add(adminUser);
            context.SaveChanges();
        }

        private bool IsContextAlreadySeed(AppDbContext context)
        {   // Maybe this check should be more detailed
            return context.Subjects.Any();
        }
    }
}