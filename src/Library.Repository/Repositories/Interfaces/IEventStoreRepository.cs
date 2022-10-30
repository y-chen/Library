using Library.Database.Entities;

namespace Library.Repository.Repositories.Interfaces
{
    public interface IEventStoreRepository
    {
        Task<EventStore> CreateEvent(EventStore eventStore);

        Task<IEnumerable<EventStore>> ReadEvents();
    }
}
