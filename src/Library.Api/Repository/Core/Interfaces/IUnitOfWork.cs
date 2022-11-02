using Library.Repository.Interfaces;

namespace Library.Repository.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository Book { get; }
        IEventStoreRepository EventStore { get; }

        Task CompleteAsync();

        Task DisposeAsync();
    }
}
