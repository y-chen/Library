using Library.Database.Entities;

namespace Library.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> CreateBookAsync(Book eventStore);

        Task<(IEnumerable<Book>, int)> ReadBooksAsync(
            string? searchTerm,
            string? orderBy,
            string orderDirection = "ASC",
            int skip = 0,
            int take = 0
        );

        Task<Book> ReadBookAsync(Guid id);
    }
}
