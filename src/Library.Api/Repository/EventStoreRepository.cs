using Library.Database;
using Library.Database.Entities;
using Library.Repository.Core;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<EventStore>> ReadEvents(
            Guid? streamId,
            string? streamName,
            bool latest = false
        )
        {
            IQueryable<EventStore> query = GetQuery();
            query = query
                .Where(x => streamId == null || x.StreamId == streamId)
                .Where(x => streamName == null || x.StreamName == streamName);

            if (latest)
            {
                query = query
                    .GroupBy(x => x.StreamId)
                    .Select(x => x.OrderByDescending(e => e.Revision).FirstOrDefault());
            }

            return await query.ToListAsync();
        }
    }
}
