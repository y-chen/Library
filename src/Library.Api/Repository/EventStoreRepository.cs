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

        public async Task<(IEnumerable<EventStore>, int)> ReadEvents(
            Guid? streamId,
            string? streamName,
            string? orderBy = "revision",
            string? orderDirection = "ASC",
            int skip = 0,
            int take = 0
        )
        {
            IQueryable<EventStore> query = GetQuery();
            query = query
                .Where(x => streamId == null || x.StreamId == streamId)
                .Where(x => streamName == null || x.StreamName == streamName);

            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "revision":
                        query =
                            orderDirection == "ASC"
                                ? query.OrderBy(store => store.Revision)
                                : query.OrderByDescending(store => store.Revision);
                        break;

                    default:
                        throw new InvalidOperationException("Invalid sorting operation");
                }
            }

            // Getting an error when calling Count()
            int count = query.ToList().Count;
            query = query.OrderBy(x => streamId);

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }

            IEnumerable<EventStore> items = await query.ToListAsync();

            return (items, count);
        }

        public async Task<EventStore> ReadLatestEvent(Guid streamId, string streamName)
        {
            return await GetQuery()
                .Where(x => x.StreamId == streamId)
                .Where(x => x.StreamName == streamName)
                .OrderByDescending(x => x.Revision)
                .FirstOrDefaultAsync();
        }
    }
}
