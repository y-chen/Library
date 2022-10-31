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
        public async Task<Book> CreateEvent([FromBody] Book book)
        {
            if (book == null)
            {
                throw new BadHttpRequestException("Body is null");
            }

            return await _bookService.CreateBook(book);
        }
    }
}
