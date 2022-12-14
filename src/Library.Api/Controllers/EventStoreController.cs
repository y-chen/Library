using Library.Core;
using Library.Dto;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("/api/event-store")]
    public class EventStoreController
    {
        private readonly IEventStoreService _eventStoreService;

        public EventStoreController(IEventStoreService eventStoreService)
        {
            this._eventStoreService = eventStoreService;
        }

        [HttpPost]
        public async Task<EventStore> CreateEvent([FromBody] EventStore eventStore)
        {
            if (eventStore == null)
            {
                throw new BadHttpRequestException("Body is null");
            }

            return await _eventStoreService.CreateEvent(eventStore);
        }

        [HttpGet]
        public async Task<Result<EventStore>> ReadEvents(
            [FromQuery] Guid? streamId,
            [FromQuery] string? streamName,
            [FromQuery] string? orderBy,
            [FromQuery] string? orderDirection,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 0
        )
        {
            return await _eventStoreService.ReadEvents(
                streamId,
                streamName,
                orderBy,
                orderDirection,
                skip,
                take
            );
        }
    }
}
