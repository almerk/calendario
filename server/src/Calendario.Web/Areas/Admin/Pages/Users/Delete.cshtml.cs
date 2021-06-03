using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Calendario.Core.Subjects;
using Calendario.Infrastructure;
using Calendario.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calendario.Web.Areas.Admin.Pages.Users
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {

        private readonly IRepository _repository;
        private readonly Configuration _configuration;

        public DeleteModel(IRepository repository, Configuration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public string Message { get; set; }
        public async Task<IActionResult> OnGetAsync([Required] string id)
        {
            var user = await _repository.GetByIdAsync<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            Message = $"Are you sure you want to delete user {user.Login}?";
            return Page();
        }
        public async Task<IActionResult> OnPostAsync([Required] string id)
        {
            var user = await _repository.GetByIdAsync<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Login == _configuration.AdminLogin)
            {
                return Forbid();
            }
            await _repository.DeleteAsync(user);
            return RedirectToPage("./Index");
        }
    }
}
