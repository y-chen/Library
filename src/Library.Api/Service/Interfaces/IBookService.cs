using Library.Dto;

namespace Library.Service.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBook(Book book);

        Task<IEnumerable<Book>> ReadBooks();
    }
}
