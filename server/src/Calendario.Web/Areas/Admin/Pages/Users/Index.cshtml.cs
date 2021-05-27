using System;
using System.Collections.Generic;
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

        public IList<IdentityUser> IdentityUsers { get; private set; }
        public IList<User> CalendarioUsers { get; private set; }
        public async Task OnGetAsync()
        {
            CalendarioUsers = await _repo.ListAsync<User>();
            IdentityUsers = await _userManager.Users.ToListAsync();
        }
    }
}
