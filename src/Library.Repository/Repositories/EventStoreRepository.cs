using AutoMapper;
using Library.Database;
using Library.Database.Entities;
using Library.Repository.Core;
using Library.Repository.Repositories.Interfaces;

namespace Library.Repository.Repositories
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
