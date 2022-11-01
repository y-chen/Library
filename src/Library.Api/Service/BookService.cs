using Library.Service.Interfaces;
using Library.Dto;
using System.Dynamic;
using System.Text.Json;

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
                JsonSerializer.Deserialize<ExpandoObject>(JsonSerializer.Serialize(book)),
                0
            );
            EventStore newStore = await _eventStoreService.CreateEvent(store);

            return JsonSerializer.Deserialize<Book>(JsonSerializer.Serialize(newStore.Data));
        }

        public async Task<IEnumerable<Book>> ReadBooks()
        {
            IEnumerable<EventStore> bookEvents = await _eventStoreService.ReadEvents(
                streamId: null,
                streamName: "Book",
                latest: true
            );

            return bookEvents.Select(
                x => JsonSerializer.Deserialize<Book>(JsonSerializer.Serialize(x.Data))
            );
        }

        public async Task<Book> ReadBookById(Guid id)
        {
            EventStore bookEvent = await _eventStoreService.ReadEvent(
                streamId: id,
                streamName: "Book"
            );

            if (bookEvent == null)
            {
                throw new KeyNotFoundException($"Book with Id {id.ToString()} not found");
            }

            return JsonSerializer.Deserialize<Book>(JsonSerializer.Serialize(bookEvent.Data));
        }
    }
}
