using AutoMapper;
using Library.Repository.Core.Interfaces;
using Library.Service.Interfaces;
using System.Text.Json;

using Entities = Library.Database.Entities;
using Dtos = Library.Dto;

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

        public async Task<Dtos.Book> CreateBook(Dtos.Book book)
        {
            if (book == null)
            {
                throw new ArgumentException("Book is null", nameof(Dtos.Book));
            }

            Guid streamId = Guid.NewGuid();
            book.Id = streamId;
            Entities.EventStore store = new Entities.EventStore(
                streamId,
                "Book",
                "Create",
                JsonSerializer.Serialize(book, new JsonSerializerOptions()),
                0
            );

            await _unitOfWork.EventStore.CreateEvent(store);
            await _unitOfWork.CompleteAsync();

            Dtos.Book newBook = JsonSerializer.Deserialize<Dtos.Book>(
                store.Data,
                new JsonSerializerOptions()
            );

            return newBook;
        }
    }
}
