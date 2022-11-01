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
            ValidateBook(book);

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

        public async Task<Book> UpdateBook(Guid id, Book book)
        {
            ValidateBook(book);

            EventStore bookEvent = await _eventStoreService.ReadEvent(
                streamId: id,
                streamName: "Book"
            );

            if (bookEvent == null)
            {
                throw new KeyNotFoundException($"Book with Id {id.ToString()} not found");
            }

            EventStore store = new EventStore(
                id,
                "Book",
                "Update",
                JsonSerializer.Deserialize<ExpandoObject>(JsonSerializer.Serialize(book)),
                ++bookEvent.Revision
            );
            EventStore newStore = await _eventStoreService.CreateEvent(store);

            return JsonSerializer.Deserialize<Book>(JsonSerializer.Serialize(newStore.Data));
        }

        private void ValidateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("Book is null", nameof(Book));
            }

            if (book.Title == null)
            {
                throw new ArgumentNullException("Title is missing");
            }

            if (book.Description == null)
            {
                throw new ArgumentNullException("Description is missing");
            }

            if (book.PublishDate == null)
            {
                throw new ArgumentNullException("Publish date is missing");
            }

            if (book.Authors?.Length < 1)
            {
                throw new ArgumentNullException("At least one author is required");
            }
        }
    }
}
