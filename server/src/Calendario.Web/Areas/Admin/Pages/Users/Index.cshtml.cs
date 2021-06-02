using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Calendario.Core.Subjects;
using Calendario.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Calendario.Web.Areas.Admin.Pages.Users
{
    [Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository _repo;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(UserManager<IdentityUser> userManager, IRepository repo, ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _repo = repo;
            _logger = logger;
        }

        public IList<IdentityUserView> IdentityUsersNotRegistered { get; private set; }
        public IList<IdentityUserView> IdentityUsers { get; private set; }
        public IList<CalendarioUserView> CalendarioUsers { get; private set; }
        public IList<CalendarioUserView> CalendarioUsersWithNoIdentity { get; private set; }
        public async Task OnGetAsync()
        {

            var users = await _repo.ListAsync<User>(includeProperties: u => u.Group);
            CalendarioUsers = (from u in users
                               select new CalendarioUserView
                               {
                                   Id = u.Id,
                                   Login = u.Login,
                                   Name = u.Name,
                                   Surname = u.Surname,
                                   Patronymic = u.Patronymic,
                                   GroupId = u.Group.Id,
                                   GroupName = u.Group.Name
                               }).ToList();
            IdentityUsers = (from u in await _userManager.Users.ToListAsync()
                             select new IdentityUserView()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email
                             }).ToList();
            var calendarioLogins = CalendarioUsers.Select(x => x.Login).ToList();
            var identityUserNames = IdentityUsers.Select(x => x.UserName);

            IdentityUsersNotRegistered = (from u in IdentityUsers
                                          where !calendarioLogins.Contains(u.UserName)
                                          select u).ToList();
            CalendarioUsersWithNoIdentity = (from u in CalendarioUsers
                                             where !identityUserNames.Contains(u.Login)
                                             select u).ToList();
        }

        public class IdentityUserView
        {
            [DisplayName("Id")]
            public string Id { get; set; }
            [DisplayName("User name")]
            public string UserName { get; set; }
            [DisplayName("User email")]
            public string Email { get; set; }
        }

        public class CalendarioUserView
        {
            [DisplayName("Id")]
            public string Id { get; set; }
            [DisplayName("Login")]
            public string Login { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string GroupId { get; set; }
            [DisplayName("Group name")]
            public string GroupName { get; set; }
        }
    }
}
