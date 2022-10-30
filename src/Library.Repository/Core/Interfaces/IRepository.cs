using Library.Database.Core;

namespace Library.Repository.Core.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task Create(T entity);

        Task<IEnumerable<T>> Read();

        Task<T> ReadById(Guid id);

        void Update(T entity);

        void Delete(T entity);
    }
}
