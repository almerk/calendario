using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Calendario.Infrastructure.Data;
using Calendario.Core.Subjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Calendario.Infrastructure.Services.Account
{
    public sealed class RegisterUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository _repository;

        public RegisterUserService(
            UserManager<IdentityUser> userManager,
            IRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public class RegisterModel
        {
            [Required]
            public string Login { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }

            public string Password { get; set; }
            public string GroupId { get; set; }
        }

        public static bool Validate(RegisterModel model, out ICollection<ValidationResult> validationResults)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);//TODO: check if i can do smth with context
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, validationResults);
        }

        public class RegisterResult
        {
            internal RegisterResult() { }

            public bool IsSuccess { get; internal set; }

            public User? Result { get; internal set; }

            public ICollection<IdentityError> IdentityErrors { get; internal set; } = new IdentityError[0];

            public ICollection<ValidationResult> ValidationResults { get; internal set; } = new ValidationResult[0];

            public override string ToString()
            {
                return "IdentityErrors: ValidationErrors";
            }

        }

        public async Task<RegisterResult> RegisterUser(RegisterModel model)
        {
            var res = new RegisterUserService.RegisterResult();
            ICollection<ValidationResult> validationResults;
            if (!Validate(model, out validationResults))
            {
                res.ValidationResults = validationResults;
                return res;
            }
            var identityResult = await RegisterIdentityUser(model);
            if (!identityResult.IsSuccess)
            {
                return identityResult;
            }
            var calendarioResult = await RegisterCalendarioUser(model);
            if(calendarioResult.IsSuccess)
            {
                await AddCalendarioClaimsToIdentity(model.Login, calendarioResult.Result);
            }
            return calendarioResult;
        }
        public async Task<RegisterResult> RegisterIdentityUser(RegisterModel model)
        {
            var res = new RegisterUserService.RegisterResult();
            ICollection<ValidationResult> validationResults;
            if (!Validate(model, out validationResults))
            {
                res.ValidationResults = validationResults;
                return res;
            }
            if (model.Password == null)
            {
                res.ValidationResults = new[] { new ValidationResult("Password is required") };
                return res;
            }
            var identityResult = await _userManager.CreateAsync(new IdentityUser() { UserName = model.Login }, model.Password);
            if (!identityResult.Succeeded)
            {
                res.IdentityErrors = identityResult.Errors.ToList();
                return res;
            }
            res.IsSuccess = true;
            return res;
        }

        public async Task<RegisterResult> RegisterCalendarioUser(RegisterModel model)
        {
            var res = new RegisterUserService.RegisterResult();
            ICollection<ValidationResult> validationResults;
            if (!Validate(model, out validationResults))
            {
                res.ValidationResults = validationResults;
                return res;
            }
            if (model.GroupId == null)
            {
                res.ValidationResults = new[] { new ValidationResult("Group field is required") };
                return res;
            }
            var group = await _repository.GetByIdAsync<Group>(model.GroupId);
            if (group is null)
                throw new ApplicationException($"Unable to find group with id {model.GroupId}."); //TODO: Add specific domain exception
            var hasUserWithSameLogin = (await _repository.ListAsync<User>(x => EF.Functions.Like(x.Login, model.Login))).Any();
            if (hasUserWithSameLogin)
            {
                res.ValidationResults = new[] { new ValidationResult("User with such login allready exists") };
                return res;
            }

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
            return res;
        }

        public async Task AddCalendarioClaimsToIdentity(string login, User calendarioUser = null)
        {
            calendarioUser ??= (await _repository.ListAsync<User>(x => EF.Functions.Like(x.Login, login))).FirstOrDefault();
            if (calendarioUser == null)
            {
                return;
            }
            var identityUser = await _userManager.FindByNameAsync(login);
            if (identityUser == null)
            {
                return;
            }
            await _userManager.AddClaimAsync(identityUser, new System.Security.Claims.Claim("calendario-user-id", calendarioUser.Id));
        }
    }
}