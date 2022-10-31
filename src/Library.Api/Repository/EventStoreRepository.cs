using System.Linq.Expressions;
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

        public async Task<IEnumerable<EventStore>> ReadEvents(Guid? streamId, string? streamName)
        {
            IList<Expression<Func<EventStore, bool>>> predicates =
                new List<Expression<Func<EventStore, bool>>>();

            if (streamId != null)
            {
                predicates.Add(x => x.StreamId == streamId);
            }

            if (streamName != null)
            {
                predicates.Add(x => x.StreamName == streamName);
            }

            return await Read(predicates);
        }
    }
}
