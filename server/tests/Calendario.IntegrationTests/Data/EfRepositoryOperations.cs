using NUnit.Framework;
using Calendario.Core;

namespace Calendario.IntegrationTests.Data
{
    public class EfRepositoryOperations: BaseEfRepoTestFixture
    {

        [Test]
        public void RepositoryCreation_Success() 
        {
            var repository = GetRepository();
            var relations = repository.ListAsync<Relation>();
        }
    }
}