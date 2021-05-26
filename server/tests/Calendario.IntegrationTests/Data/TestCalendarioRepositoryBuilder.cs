using System;
using Calendario.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Calendario.IntegrationTests.Data
{
    //https://docs.microsoft.com/ru-ru/ef/core/testing/sqlite
    //https://github.com/almerk/calendario-proto/blob/master/tests/Calendario.IntegrationTests/Data/BaseEfRepoTestFixture.cs

    public class TestCalendarioRepositoryBuilder : IDisposable
    {
        protected AppDbContext _dbContext;
        protected SqliteConnection _connection;

        public void Dispose()
        {
            _connection?.Dispose();
            _dbContext?.Dispose();
        }

        protected DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlite()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlite(_connection)
                    //.LogTo(System.Console.WriteLine)
                    //.LogTo(TestContext.WriteLine)
                    .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        public EfRepository Build()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            var options = CreateNewContextOptions();
            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();
            return new EfRepository(_dbContext);
        }
    }
}