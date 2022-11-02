using Library.Core;
using Library.Dto;

namespace Library.Service.Interfaces
{
    public interface IEventStoreService
    {
        Task<EventStore> CreateEvent(EventStore eventStore);

        Task<Result<EventStore>> ReadEvents(
            Guid? streamId,
            string? streamName,
            bool latest = false,
            int skip = 0,
            int take = 0
        );

        Task<EventStore> ReadLatestEvent(Guid streamId, string streamName);
    }
}
