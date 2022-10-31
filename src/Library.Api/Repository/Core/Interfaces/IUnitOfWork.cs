using Library.Repository.Interfaces;

namespace Library.Repository.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IEventStoreRepository EventStore { get; }

        Task CompleteAsync();

        Task DisposeAsync();
    }
}
