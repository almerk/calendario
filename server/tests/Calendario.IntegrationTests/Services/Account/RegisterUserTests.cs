using NUnit.Framework;
using Calendario.Core;
using Calendario.Core.Subjects;
using Calendario.IntegrationTests.Data;
using Calendario.Infrastructure.Services.Account;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using Calendario.Infrastructure.Data;
using Calendario.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Calendario.IntegrationTests.Services.Account
{
    public class RegisterUserTests
    {
        IServiceProvider _provider;

        [SetUp]
        public void SetUp()
        {
            var repo = new TestCalendarioRepositoryBuilder().Build();
            _provider = new ServiceProviderBuilder()
                        .ConfigureServiceCollection(services =>
                        {
                            services.AddScoped<IRepository, EfRepository>(p => repo);
                            services.AddMockedUserManager();
                            services.AddScoped<RegisterUserService>();
                        })
                        .Build();
        }

        [Test]
        public async Task AddingCalendarioUser_RegisterResultSuccessAndContainsUser()
        {
            var repository = _provider.GetService<IRepository>();
            var group = await repository.AddAsync(new Group() { Name = "TestGroup" });
            var registerUserService = _provider.GetService<RegisterUserService>();
            var model = new RegisterUserService.RegisterModel()
            {
                Login = "TestUser",
                Name = "User",
                GroupId = group.Id,
                Password = "123"
            };
            var result = await registerUserService.RegisterCalendarioUser(model);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.NotNull(result.Result.Id);
        }

        [Test]
        public async Task AddingUserWithNoGroup_ValidationFails()
        {
            var registerUserService = new RegisterUserService(null, null);
            var model = new RegisterUserService.RegisterModel()
            {
                Login = "TestUser",
                Name = "User",
                GroupId = null,
                Password = "123"
            };
            var result = await registerUserService.RegisterCalendarioUser(model);
            Assert.False(result.IsSuccess);
            Assert.True(result.ValidationResults.Any());

        }
        [Test]
        public async Task AddingUserWithNoName_ValidationFails()
        {
            var registerUserService = new RegisterUserService(null, null);
            var model = new RegisterUserService.RegisterModel()
            {
                Login = "TestUser",
                Name = null,
                GroupId = "null",
                Password = "123"
            };
            var result = await registerUserService.RegisterCalendarioUser(model);
            Assert.False(result.IsSuccess);
            Assert.True(result.ValidationResults.Any());

        }
        [Test]
        public async Task AddingUserWithNoLogin_ValidationFails()
        {
            var registerUserService = new RegisterUserService(null, null);
            var model = new RegisterUserService.RegisterModel()
            {
                Login = null,
                Name = "User",
                GroupId = "1",
                Password = "123"
            };
            var result = await registerUserService.RegisterCalendarioUser(model);
            Assert.False(result.IsSuccess);
            Assert.True(result.ValidationResults.Any());
        }



        [Test]
        public async Task AddingCalendarioUserWithNotExistedGroupId_ThrowsException()
        {
            var repository = _provider.GetService<IRepository>();
            var group = await repository.AddAsync(new Group() { Name = "TestGroup" });
            var registerUserService = new RegisterUserService(null, repository);

            var model = new RegisterUserService.RegisterModel()
            {
                Login = "TestUser",
                GroupId = group.Id + "1",//Surely different from existing
                Password = "123",
                Name = "User",
            };
            Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await registerUserService.RegisterCalendarioUser(model);
            });

        }
        [Test]
        public async Task AddingCalendarioUserWithSameLogin_ValidationFails()
        {
            var repository = _provider.GetService<IRepository>();
            var group = await repository.AddAsync(new Group() { Name = "TestGroup" });
            var registerUserService = new RegisterUserService(null, repository);

            var model = new RegisterUserService.RegisterModel()
            {
                Login = "TestUser",
                GroupId = group.Id,
                Password = "123",
                Name = "User",
            };
            await registerUserService.RegisterCalendarioUser(model);

            var sameLoginModel = new RegisterUserService.RegisterModel()
            {
                Login = "testUser",
                GroupId = group.Id,
                Password = "1213",
                Name = "User2",
            };
            var result = await registerUserService.RegisterCalendarioUser(sameLoginModel);
            Assert.False(result.IsSuccess);
            Assert.True(result.ValidationResults.Any());
        }
    }
}