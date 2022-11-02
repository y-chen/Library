using Library.Core;
using Library.Dto;

namespace Library.Service.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBookAsync(Book book);

        Task<Result<Book>> ReadBooksAsync(
            string? searchTerm,
            string? orderBy,
            string orderDirection = "ASC",
            int skip = 0,
            int take = 0
        );

        Task<Book> ReadBookByIdAsync(Guid id);

        Task<Book> UpdateBookAsync(Guid id, Book book);
    }
}
