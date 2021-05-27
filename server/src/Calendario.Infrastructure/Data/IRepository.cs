
using Calendario.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Calendario.Infrastructure.Data
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(string id) where T : Entity;
        Task<List<T>> ListAsync<T>(
            System.Linq.Expressions.Expression<Func<T, bool>> queryPredicate = null,
            params Expression<Func<T, object>>[] includeProperties) where T : class;
        Task<T> AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : Entity;
        Task DeleteAsync<T>(T entity) where T : class;
    }
}

