using AutoMapper;
using Library.Repository.Core.Interfaces;
using Library.Service.Interfaces;
using Library.Dto;
using Newtonsoft.Json;
using System.Dynamic;

using Entities = Library.Database.Entities;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> CreateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentException("Book is null", nameof(Book));
            }

            Guid streamId = Guid.NewGuid();
            book.Id = streamId;
            Entities.EventStore store = new Entities.EventStore(
                streamId,
                "Book",
                "Create",
                JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(book)),
                0
            );

            await _unitOfWork.EventStore.CreateEvent(store);
            await _unitOfWork.CompleteAsync();

            Book newBook = JsonConvert.DeserializeObject<Book>(
                JsonConvert.SerializeObject(store.Data)
            );

            return newBook;
        }

        public async Task<IEnumerable<Book>> ReadBooks()
        {
            IEnumerable<Entities.EventStore> bookEvents = await _unitOfWork.EventStore.ReadEvents(
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
