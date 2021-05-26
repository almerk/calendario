using System;
using System.Linq;
using System.Threading.Tasks;
using Calendario.Core.Subjects;
using Calendario.Infrastructure.Data;
using Calendario.Infrastructure.Services.Account;

namespace Calendario.Infrastructure.Services
{
    public class InitialDbSeedService
    {

        private Configuration _configuration;
        private RegisterUserService _registrationService;
        private AppDbContext _context;

        public InitialDbSeedService(
            Configuration configuration,
            RegisterUserService registrationService,
            AppDbContext context)
        {
            _configuration = configuration;
            _registrationService = registrationService;
            _context = context;
        }

        public async Task Seed()
        {
            if (IsAllreadySeed()) return;
            var rootGroup = new Group()
            {
                Name = "All"
            };
            await _context.AddAsync(rootGroup);
            await _context.SaveChangesAsync();
            var adminUser = new RegisterUserService.RegisterModel()
            {
                GroupId = rootGroup.Id,
                Login = _configuration.AdminLogin,
                Name = _configuration.AdminName,
                Password = _configuration.AdminPassword
            };
            var result = await _registrationService.RegisterUser(adminUser);
            if (!result.IsSuccess)
            {
                throw new ApplicationException($"Unable to seed database with admin user: {result.ToString()}");
            }
        }

        private bool IsAllreadySeed()
        {   // Maybe this check should be more detailed
            return _context.Subjects.Any();
        }
    }
}