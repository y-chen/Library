using Library.Dto;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("/api/books")]
    public class BookController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpPost]
        public async Task<Book> CreateBook([FromBody] Book book)
        {
            if (book == null)
            {
                throw new BadHttpRequestException("Body is null");
            }

            return await _bookService.CreateBook(book);
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> ReadBooks()
        {
            return await _bookService.ReadBooks();
        }

        [HttpGet("{id}")]
        public async Task<Book> ReadBookById([FromRoute] Guid id)
        {
            return await _bookService.ReadBookById(id);
        }
    }
}
