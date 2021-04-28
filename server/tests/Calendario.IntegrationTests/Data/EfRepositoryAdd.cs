using NUnit.Framework;
using Calendario.Core;

namespace Calendario.IntegrationTests.Data
{
    public class EfRepositoryAdd: BaseEfRepoTestFixture
    {

        [Test]
        public void AddsItemAndSetsId() 
        {
            var repository = GetRepository();
            var relations = repository.ListAsync<Relation>();
        }
    }
}