using NUnit.Framework;
using Calendario.Core;
using Calendario.Core.Subjects;
using Calendario.IntegrationTests.Data;
using Calendario.Infrastructure.Data;
using Calendario.Infrastructure.Services.Account;
using System.Threading.Tasks;

namespace Calendario.IntegrationTests.Services.Account
{
    public class RegisterUserTests : BaseEfRepoTestFixture
    {

        //public IRepository SeededRepository => GetRepository();

        [Test]
        public async Task AddingCalendarioUserWithNoExistedGroup_RegisterResultNotSuccessAndHasErrors()
        {
            var repository = GetRepository();
            var groupId = await repository.AddAsync(new Group() { Name = "TestGroup" });
            var registerUserService = new RegisterUserService(null, null, repository);

            var model = new RegisterUserService.RegisterModel()
            {
                UserName = "Test User",
                GroupId = null,
                Password = "123"
            };
            var result = await registerUserService.RegisterUserWithNoIdentity(model);
            Assert.False(result.Success);
        }

        public void AddingExistingCalendarioUser_RegisterResultNotSuccessAndHasErrors()
        {
            var repository = GetRepository();

        }
    }
}