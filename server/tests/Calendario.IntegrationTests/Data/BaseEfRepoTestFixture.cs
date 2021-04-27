
using Calendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Calendario.IntegrationTests.Data
{
    //https://docs.microsoft.com/ru-ru/ef/core/testing/sqlite
    //https://github.com/almerk/calendario-proto/blob/master/tests/Calendario.IntegrationTests/Data/BaseEfRepoTestFixture.cs

    public abstract class BaseEfRepoTestFixture
    {
        protected AppDbContext _dbContext;

        protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlite()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlite("Filename=:memory:")
                    .LogTo(System.Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected EfRepository GetRepository()
        {
            var options = CreateNewContextOptions();
            _dbContext = new AppDbContext(options);
            return new EfRepository(_dbContext);
        }
    }
}