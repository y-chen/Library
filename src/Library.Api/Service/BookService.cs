using Library.Service.Interfaces;
using Library.Dto;
using Newtonsoft.Json;
using System.Dynamic;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly IEventStoreService _eventStoreService;

        public BookService(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task<Book> CreateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentException("Book is null", nameof(Book));
            }

            Guid streamId = Guid.NewGuid();
            book.Id = streamId;
            EventStore store = new EventStore(
                streamId,
                "Book",
                "Create",
                JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(book)),
                0
            );
            EventStore newStore = await _eventStoreService.CreateEvent(store);

            return JsonConvert.DeserializeObject<Book>(JsonConvert.SerializeObject(newStore.Data));
        }

        public async Task<IEnumerable<Book>> ReadBooks()
        {
            IEnumerable<EventStore> bookEvents = await _eventStoreService.ReadEvents(
                streamId: null,
                streamName: "Book",
                latest: true
            );

            return bookEvents.Select(
                x => JsonConvert.DeserializeObject<Book>(JsonConvert.SerializeObject(x.Data))
            );
        }
    }
}
