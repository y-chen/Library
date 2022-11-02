using Library.Database;
using Library.Database.Entities;
using Library.Repository.Core;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context) { }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await Create(book);

            return book;
        }

        public async Task<(IEnumerable<Book>, int)> ReadBooksAsync(
            string? searchTerm,
            string? orderBy,
            string? orderDirection = "ASC",
            int skip = 0,
            int take = 0
        )
        {
            IQueryable<Book> query = GetQuery();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(
                    book =>
                        book.Title.ToLower().Contains(searchTerm)
                        || book.Description.ToLower().Contains(searchTerm)
                        || book.Author.ToLower().Contains(searchTerm)
                );
            }

            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "title":
                        query =
                            orderDirection == "ASC"
                                ? query.OrderBy(book => book.Title)
                                : query.OrderByDescending(book => book.Title);
                        break;
                    case "description":
                        query =
                            orderDirection == "ASC"
                                ? query.OrderBy(book => book.Description)
                                : query.OrderByDescending(book => book.Description);
                        break;
                    case "publishDate":
                        query =
                            orderDirection == "ASC"
                                ? query.OrderBy(book => book.PublishDate)
                                : query.OrderByDescending(book => book.PublishDate);
                        break;
                    case "author":
                        query =
                            orderDirection == "ASC"
                                ? query.OrderBy(book => book.Author)
                                : query.OrderByDescending(book => book.Author);
                        break;

                    default:
                        throw new InvalidOperationException("Invalid sorting operation");
                }
            }

            // Getting an error when calling Count()
            int count = (await query.ToListAsync()).Count;

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }

            IEnumerable<Book> items = await query.ToListAsync();

            return (items, count);
        }

        public async Task<Book> ReadBookByIdAsync(Guid id)
        {
            return await GetQuery().Where(book => book.Id == id).FirstOrDefaultAsync();
        }

        public Book UpdateBookAsync(Guid id, Book book)
        {
            Update(book);

            return book;
        }
    }
}
