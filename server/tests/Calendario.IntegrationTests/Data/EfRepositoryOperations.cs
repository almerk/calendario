using NUnit.Framework;
using Calendario.Core;
using Calendario.Core.Subjects;
using System.Threading.Tasks;
using Calendario.Infrastructure.Data;

namespace Calendario.IntegrationTests.Data
{
    public class EfRepositoryOperations
    {
        protected EfRepository CreateRepository() => new TestCalendarioRepositoryBuilder()
                                                    .Build();

        [Test]
        public async Task GroupAdding_SuccessAndCountEqualsOne()
        {
            int groupsCount;
            using (var repository = CreateRepository())
            {
                var groupId = await repository.AddAsync(new Group() { Name = "Test group" });
                var groups = await repository.ListAsync<Group>();
                groupsCount = groups.Count;
            }
            Assert.AreEqual(1, groupsCount);
        }
    }
}