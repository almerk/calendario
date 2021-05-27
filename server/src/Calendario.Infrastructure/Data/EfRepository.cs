using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Calendario.Core.Base;
using Microsoft.EntityFrameworkCore;

namespace Calendario.Infrastructure.Data
{
    public class EfRepository : IRepository, IDisposable
    {
        private readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<T> GetByIdAsync<T>(string id) where T : Entity
        {
            return _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }
        public T GetById<T>(string id) where T : Entity
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }
        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }



        public async Task UpdateAsync<T>(T entity) where T : Entity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public Task<List<T>> ListAsync<T>(
                                Expression<Func<T, bool>> queryPredicate = null,
                                params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> values = _dbContext.Set<T>().AsNoTracking();
            if(includeProperties.Any())
            {
                values = includeProperties.Aggregate(values, (current, i) => current.Include(i));
            }
            if (queryPredicate != null)
            {
                values = values.Where(queryPredicate);
            }
            return values.ToListAsync();
        }
    }
}