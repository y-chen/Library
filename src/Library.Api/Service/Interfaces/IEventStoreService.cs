using Library.Dto;

namespace Library.Service.Interfaces
{
    public interface IEventStoreService
    {
        Task<EventStore> CreateEvent(EventStore eventStore);

        Task<IEnumerable<EventStore>> ReadEvents(Guid? streamId, string? streamName);
    }
}
