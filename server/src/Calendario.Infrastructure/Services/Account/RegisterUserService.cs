using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Calendario.Infrastructure.Data;
using Calendario.Core.Subjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Calendario.Infrastructure.Services.Account
{
    public sealed class RegisterUserService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository _repository;

        public RegisterUserService(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IRepository repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _repository = repository;
        }

        public class RegisterModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string GroupId { get; set; }
        }

        public class RegisterResult
        {
            private RegisterResult() { }

            public bool Success { get; init; }

            public User Result { get; init; }

            public IEnumerable<IdentityError> IdentityErrors { get; set; } = new IdentityError[0];

        }

        public async Task<RegisterResult> RegisterUserWithNewIdentity(RegisterModel model)
        {
            //TODO: Check if calendario user allready exists
            throw new NotImplementedException();
        }
        public async Task<RegisterResult> RegisterUserWithExistedIdentity(RegisterModel model, IdentityUser user = null)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterResult> RegisterUserWithNoIdentity(RegisterModel model)
        {
                      
            throw new NotImplementedException();
        }

    }
}