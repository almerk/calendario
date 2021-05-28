using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Calendario.Core.Subjects;
using Calendario.Infrastructure.Data;
using Calendario.Infrastructure.Services.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Calendario.Web.Areas.Admin.Pages.Users
{
    [AllowAnonymous]
    //TODO: Auth this page only for admin
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RegisterUserService _registerService;
        private readonly IRepository _repository;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            ILogger<RegisterModel> logger,
            RegisterUserService registerService,
            IRepository repository)
        {
            _userManager = userManager;
            _logger = logger;
            _registerService = registerService;
            _repository = repository;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {

            [Required]
            [DisplayName("Group")]
            public string GroupId { get; set; }

            [Required]
            [Display(Name = "Login")]
            public string Login { get; set; }

            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [DisplayName("Is Identity user required")]
            public bool IsIdentityRequired { get; set; } = true;
        }

        public SelectList GroupsSL { get; set; }

        public async Task OnGetAsync(string userName = null, string calendarioId = null)
        {
            await PopulateGroupsSelectList();
            if (userName != null)
            {
                var identityUser = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
                if (identityUser == null)
                    throw new ApplicationException($"Unable to get identity user with username ${userName}.");
                Input.Login = userName;
                Input.IsIdentityRequired = false;
            }
        }

        private async Task PopulateGroupsSelectList()
        {
            var groups = await _repository.ListAsync<Group>();
            GroupsSL = new SelectList(groups, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                var user = new RegisterUserService.RegisterModel()
                {
                    GroupId = Input.GroupId,
                    Login = Input.Login,
                    Name = Input.Name,
                    Password = Input.Password,
                    Patronymic = Input.Patronymic,
                    Surname = Input.Surname

                };

                var result = Input.IsIdentityRequired ?
                        await _registerService.RegisterUser(user) :
                        await _registerService.RegisterCalendarioUser(user);

                if (result.IsSuccess)
                {
                    _logger.LogInformation($"Created calendario user with new identity: {Input.Login}.");
                    return RedirectToPage("./Index");
                }
                else
                {
                    foreach (var error in result.ValidationResults)
                    {
                        ModelState.AddModelError("Validation Error", error.ErrorMessage);
                    }
                    foreach (var error in result.IdentityErrors)
                    {
                        ModelState.AddModelError("Identity Error", error.Description);
                    }
                }
            }
            await PopulateGroupsSelectList();
            return Page();
        }
    }
}
