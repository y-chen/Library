using Library.Database.Entities;

namespace Library.Repository.Interfaces
{
    public interface IEventStoreRepository
    {
        Task<EventStore> CreateEvent(EventStore eventStore);

        Task<(IEnumerable<EventStore>, int)> ReadEvents(
            Guid? streamId,
            string? streamName,
            bool latest = false,
            int skip = 0,
            int take = 0
        );

        Task<EventStore> ReadEvent(Guid streamId, string streamName);
    }
}
