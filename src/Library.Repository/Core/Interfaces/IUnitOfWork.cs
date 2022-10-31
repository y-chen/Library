using Library.Repository.Repositories.Interfaces;

namespace Library.Repository.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IEventStoreRepository EventStore { get; }

        Task CompleteAsync();

        Task DisposeAsync();
    }
}
