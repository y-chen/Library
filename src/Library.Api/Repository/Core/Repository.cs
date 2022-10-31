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
            this.dbSet = context.Set<T>();
        }

        public async Task Create(T entity)
        {
            entity.CreatedBy = "Anonymous";
            entity.CreatedAt = DateTime.UtcNow;

            await this.dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> Read()
        {
            return await this.dbSet.ToListAsync();
        }

        public async Task<T> ReadById(Guid id)
        {
            return await this.dbSet.SingleAsync(entity => entity.Id == id);
        }

        public void Update(T entity)
        {
            this.dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }
    }
}
