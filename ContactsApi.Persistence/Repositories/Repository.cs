using ContactsApi.Core.Entities;
using ContactsApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactsApi.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected ContactsApiDbContext DbContext;

        public Repository(ContactsApiDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbContext.Set<T>().FindAsync(id);
            await DeleteAsync(entity);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate);
        }
    }
}
