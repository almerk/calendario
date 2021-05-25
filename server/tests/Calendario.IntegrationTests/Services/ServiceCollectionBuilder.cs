using System;
using Microsoft.Extensions.DependencyInjection;

namespace Calendario.IntegrationTests.Services
{
    public class ServiceProviderBuilder
    {
        public ServiceCollection _collection= new ServiceCollection();

        public ServiceProviderBuilder ConfigureServiceCollection(Action<IServiceCollection> action)
        {
            action(_collection);
            return this;
        }

        public IServiceProvider Build()
        {
            return _collection.BuildServiceProvider();
        }
    }
}
