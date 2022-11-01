using AutoMapper;
using Library.Dto;
using Library.Repository.Core.Interfaces;
using Library.Service.Interfaces;

using EventStoreDto = Library.Dto.EventStore;
using EventStoreEntity = Library.Database.Entities.EventStore;

namespace Library.Service
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventStoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EventStoreDto> CreateEvent(EventStoreDto eventStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentException("EventStore is null", nameof(EventStore));
            }

            EventStoreEntity entity = _mapper.Map<EventStoreEntity>(eventStore);
            await _unitOfWork.EventStore.CreateEvent(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EventStoreDto>(entity);
        }

        public async Task<IEnumerable<EventStoreDto>> ReadEvents(
            Guid? streamId,
            string? streamName,
            bool latest = false
        )
        {
            IEnumerable<EventStoreEntity> entities = await _unitOfWork.EventStore.ReadEvents(
                streamId,
                streamName,
                latest
            );

            return entities.Select(entity => _mapper.Map<EventStoreDto>(entity));
        }

        public async Task<EventStoreDto> ReadEvent(Guid streamId, string streamName)
        {
            EventStoreEntity entity = await _unitOfWork.EventStore.ReadEvent(streamId, streamName);

            return _mapper.Map<EventStoreDto>(entity);
        }
    }
}
