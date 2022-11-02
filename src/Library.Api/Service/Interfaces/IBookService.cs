using Library.Core;
using Library.Dto;

namespace Library.Service.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBook(Book book);

        Task<Result<Book>> ReadBooks(int skip, int take);

        Task<Book> ReadBookById(Guid id);

        Task<Book> UpdateBook(Guid id, Book book);
    }
}
