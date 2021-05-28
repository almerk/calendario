using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Calendario.Core.Subjects;
using Calendario.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Calendario.Web.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {
        //TODO: Show list of users with no identity
        //TODO: Show list of identity users with no calendario account
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository _repo;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(UserManager<IdentityUser> userManager, IRepository repo, ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _repo = repo;
            _logger = logger;
        }

        public IList<IdentityUserView> IdentityUsers { get; private set; }
        public IList<CalendarioUserView> CalendarioUsers { get; private set; }
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

            IdentityUsers = (from u in _userManager.Users.ToList()
                             select new IdentityUserView()
                             {
                                 Id = u.Id,
                                 UserName = u.UserName,
                                 Email = u.Email
                             }).ToList();
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
