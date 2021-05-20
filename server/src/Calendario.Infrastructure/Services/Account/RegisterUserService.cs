using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Calendario.Infrastructure.Data;
using Calendario.Core.Subjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


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
            public string Login { get; set; }
            [Required]
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }

            public string Password { get; set; }

            [Required]
            public string GroupId { get; set; }
        }

        public static bool Validate(RegisterModel model, out ICollection<ValidationResult> validationResults)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);//TODO: check if i can do smth with context
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(model,context, validationResults);
        }

        public class RegisterResult
        {
            internal RegisterResult() { }

            public bool IsSuccess { get; internal set; }

            public User Result { get; internal set; }

            public ICollection<IdentityError> IdentityErrors { get; internal set; } = new IdentityError[0];

            public ICollection<ValidationResult> ValidationResults { get; internal set; } = new ValidationResult[0];
            public Exception InnerException { get; internal set; }

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
            var res = new RegisterUserService.RegisterResult();
            ICollection<ValidationResult> validationResults;
            if(!Validate(model, out validationResults))
            {
                res.ValidationResults = validationResults;
                return res;
            }
            try
            {
                var group = await _repository.GetByIdAsync<Group>(model.GroupId);
                if (group is null)
                    throw new ApplicationException($"Unable to find group with id {model.GroupId}.");//TODO: Add specific domain exception
                var newUser = new User()
                {
                    Login = model.Login,
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Group = group
                };
                newUser = await _repository.AddAsync(newUser);
                res.IsSuccess = true;
                res.Result = newUser;
            }
            catch (Exception ex)//TODO: Handle only specific exceptions (Such as unique constraint)
            {
                res.InnerException = ex;
            }
            return res;
        }

    }
}