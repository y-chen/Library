using Library.Database;
using Library.Database.Entities;
using Library.Repository.Core;
using Library.Repository.Interfaces;

namespace Library.Repository
{
    public class EventStoreRepository : Repository<EventStore>, IEventStoreRepository
    {
        public EventStoreRepository(LibraryContext context) : base(context) { }

        public async Task<EventStore> CreateEvent(EventStore eventStore)
        {
            await Create(eventStore);

            return eventStore;
        }

        public async Task<IEnumerable<EventStore>> ReadEvents()
        {
            return await Read();
        }
    }
}
