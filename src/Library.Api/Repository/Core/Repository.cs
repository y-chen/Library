using System.Linq.Expressions;
using Library.Database;
using Library.Database.Core;
using Library.Repository.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Core
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbSet<T> dbSet;

        protected Repository(LibraryContext context)
        {
            dbSet = context.Set<T>();
        }

        public async Task Create(T entity)
        {
            entity.CreatedBy = "Anonymous";
            entity.CreatedAt = DateTime.UtcNow;

            await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> Read()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> ReadById(Guid id)
        {
            return await dbSet.SingleAsync(entity => entity.Id == id);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IQueryable<T> GetQuery()
        {
            return dbSet.AsQueryable();
        }
    }
}
