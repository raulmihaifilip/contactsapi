using ContactsApi.Core.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactsApi.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(int id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
