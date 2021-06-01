using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Calendario.Core.Subjects;
using Calendario.Infrastructure.Data;
using Calendario.Infrastructure.Services.Account;
using Calendario.Web.Areas.Admin.Pages.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Calendario.Web.Areas.Admin.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<EditModel> _logger;
        private readonly RegisterUserService _registerService;
        private readonly IRepository _repository;

        public EditModel(
            UserManager<IdentityUser> userManager,
            ILogger<EditModel> logger,
            RegisterUserService registerService,
            IRepository repository
            )
        {
            _userManager = userManager;
            _logger = logger;
            _registerService = registerService;
            _repository = repository;
        }

        public class CalendarioUserVM
        {
            [Required]
            public string Id { get; set; }
            [Required]
            public string Login { get; set; }
            public bool HasIdentity { get; set; }
            [Required]
            public string GroupId { get; set; }
            [Required]
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            [Display(Name = "New password")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        [BindProperty]
        public CalendarioUserVM View { get; set; }
        public SelectList GroupsSL { get; set; }

        public async Task<IActionResult> OnGetAsync([Required] string userId)
        {
            await PopulateGroupsSelectList();
            var user = await _repository.GetByIdAsync<User>(userId, user => user.Group);
            if (user == null)
            {
                return NotFound();
            }
            var identityUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == user.Login);
            bool hasIdentity = identityUser != null;
            View = new CalendarioUserVM()
            {
                Id = user.Id,
                Login = user.Login,
                GroupId = user.Group.Id,
                HasIdentity = hasIdentity,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var group = await _repository.GetByIdAsync<Group>(View.GroupId);
                if (group == null)
                {
                    throw new ApplicationException("Unable to find selected group.");
                }
                var editedUser = new User()
                {
                    Id = View.Id,
                    Group = group,
                    Login = View.Login,
                    Name = View.Name,
                    Surname = View.Surname,
                    Patronymic = View.Patronymic
                };
                await _repository.UpdateAsync(editedUser);
            }
            await PopulateGroupsSelectList();
            return RedirectToPage("Index");
        }


        private async Task PopulateGroupsSelectList()
        {
            var groups = await _repository.ListAsync<Group>();
            GroupsSL = new SelectList(groups, "Id", "Name");
        }
    }
}
