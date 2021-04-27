using NUnit.Framework;

namespace Calendario.IntegrationTests.Data
{
    public class EfRepositoryAdd: BaseEfRepoTestFixture
    {

        [Test]
        public void AddsItemAndSetsId()
        {
            var repository = GetRepository();
        }
    }
}