using AutoMapper;
using Library.Core;
using Library.Dto;
using Library.Repository.Core.Interfaces;
using Library.Service.Interfaces;
using System.Dynamic;
using System.Text.Json;

using BookDto = Library.Dto.Book;
using BookEntity = Library.Database.Entities.Book;
using EventStoreEntity = Library.Database.Entities.EventStore;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDto> CreateBookAsync(Book book)
        {
            ValidateBook(book);

            BookEntity bookEntity = _mapper.Map<BookEntity>(book);
            BookEntity newBookEntity = await this._unitOfWork.Book.CreateBookAsync(bookEntity);
            EventStoreEntity store = new EventStoreEntity(
                streamId: newBookEntity.Id,
                streamName: "Book",
                eventType: "Create",
                data: JsonSerializer.Deserialize<ExpandoObject>(
                    JsonSerializer.Serialize(newBookEntity)
                ),
                revision: 0
            );
            await _unitOfWork.EventStore.CreateEvent(store);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookDto>(newBookEntity);
        }

        public async Task<Result<BookDto>> ReadBooksAsync(
            string? searchTerm,
            string? orderBy,
            string? orderDirection = "ASC",
            int skip = 0,
            int take = 0
        )
        {
            var (books, count) = await _unitOfWork.Book.ReadBooksAsync(
                searchTerm,
                orderBy,
                orderDirection,
                skip,
                take
            );

            return new Result<BookDto>(books.Select(book => _mapper.Map<BookDto>(book)), count);
        }

        public async Task<BookDto> ReadBookByIdAsync(Guid id)
        {
            BookEntity bookEntity = await _unitOfWork.Book.ReadBookByIdAsync(id);

            if (bookEntity == null)
            {
                throw new KeyNotFoundException($"Book with Id {id} not found");
            }

            return _mapper.Map<BookDto>(bookEntity);
        }

        public async Task<BookDto> UpdateBookAsync(Guid id, BookDto book)
        {
            ValidateBook(book);

            BookEntity bookEntity = await _unitOfWork.Book.ReadBookByIdAsync(id);

            if (bookEntity == null)
            {
                throw new KeyNotFoundException($"Book with Id {id.ToString()} not found");
            }

            bookEntity.Title = book.Title;
            bookEntity.Description = book.Description;
            bookEntity.PublishDate = book.PublishDate;
            bookEntity.Author = book.Author;

            BookEntity updatedBook = _unitOfWork.Book.UpdateBookAsync(id, bookEntity);
            EventStoreEntity latestEvent = await _unitOfWork.EventStore.ReadLatestEvent(
                bookEntity.Id,
                "Book"
            );
            EventStoreEntity newEvent = new EventStoreEntity(
                streamId: latestEvent.StreamId,
                streamName: latestEvent.StreamName,
                eventType: "Update",
                data: JsonSerializer.Deserialize<ExpandoObject>(
                    JsonSerializer.Serialize(bookEntity)
                ),
                revision: latestEvent.Revision + 1
            );
            await _unitOfWork.EventStore.CreateEvent(newEvent);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookDto>(updatedBook);
        }

        private void ValidateBook(BookDto book)
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

            if (book.Author == null)
            {
                throw new ArgumentNullException("Author is missing");
            }
        }
    }
}
