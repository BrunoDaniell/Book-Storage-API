using Book_API.Model;
using Book_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Book_API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>>GetBooks(int id)
        {
            return await _bookRepository.Get(id);
        }
        [HttpPost]
        public  async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new {id = newBook.Id}, newBook);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>>Delete(int id)
        {
            var bookToDelete = await _bookRepository.Get(id);
            if(bookToDelete == null)
                return NotFound();
            await _bookRepository.Delete(bookToDelete.Id);
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> PutBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest();
            await _bookRepository.Update(book);
            return NoContent(); 
        }
    }
}
