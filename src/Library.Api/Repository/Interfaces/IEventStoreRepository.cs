using Library.Database.Entities;

namespace Library.Repository.Interfaces
{
    public interface IEventStoreRepository
    {
        Task<EventStore> CreateEvent(EventStore eventStore);

        Task<IEnumerable<EventStore>> ReadEvents(
            Guid? streamId,
            string? streamName,
            bool latest = false
        );

        Task<EventStore> ReadEvent(Guid streamId, string streamName);
    }
}
