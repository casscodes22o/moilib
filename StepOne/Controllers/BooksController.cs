using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StepOne.Data;
using StepOne.Models;
/*
 LITTERED WITH COMMENTS STILL LEARNING
 Reference: https://www.youtube.com/watch?v=38GNKtclDdE&t=238s 
 NOTE TO SELF, MIND THE MATCHING OF BRACKETS ETC. 
*/

namespace StepOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // static, when list is created once, changes saved for this list whenever API is invoked
        //private static List<Book> books = new List<Book>
        //{
        //    new Book
        //    {
        //        Id = 1,
        //        Title = "Le Grand Meaulnus",
        //        Author = "Alain Henri",
        //        Purchase = Bought.ONLINE
        //    },
        //    new Book
        //    {
        //        Id = 2,
        //        Title = "The Summer of Katya",
        //        Author = "Trevanian",
        //        Purchase = Bought.PSHOP,
        //    },
        //    new Book
        //    {
        //        Id = 3,
        //        Title = "A Song of Ice and Fire: A Game of Thrones",
        //        Author = "George R.R. Martin",
        //        Purchase = Bought.BNEW
        //    },
        //    new Book
        //    {
        //        Id = 4,
        //        Title = "Terrible Tudors",
        //        Author = "Terry Deary",
        //        Purchase = Bought.PSHOP
        //    }
        //};

        private readonly BookAPIContext _context;
        public BooksController(BookAPIContext context)
        {
            _context = context;
        }

        // GET retrieves information 
        // ActionResul returns status code and result of request 
        // ActionResult {stuffToReturn} {methodName}
        // add `async` so it doesnt block thread and wrap it in Task<>
        [HttpGet]
        public async Task<ActionResult<List<Book>>> getBookList()
        {
            return Ok(await _context.Books.ToListAsync()); //Ok gives 200 status code
            // to ListAsync gets all the books in the database and returns as a list
        }

        //// GATES this parameter next to Httpget spawns what to put next to "book"/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> getBookById(int id)
        {
            // FirstOrDefault for 
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(); //404 status code

            return Ok(book); //200 status code
        }
        ////KEY Learning (?) you preempt the status code; i.e. here use of 
        ////Ok() and NotFound() API does not decide on its own(?) 
        ////HENCE, look into these status code class  

        //POST user send info 
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if(newBook == null)
                return BadRequest(); //400 status code, bad request
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync(); //saves changes to database, async so it doesnt block thread
            return CreatedAtAction(nameof(getBookById), new { id = newBook.Id }, newBook);
            //201 status code, created, and returns the new book with its id
        }

        // function above is Gemini-ed, boang why you an ID naka autoincrement sya HAHAHAAHAH

        ////PUT
        //// mind the parameters, update only works if the id is included in URL 
        [HttpPut("{id}")]
        public async Task<IActionResult> updateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(); //404 status code

            // book.Id = updatedBook.Id; REMOVE ID  bc u already have it
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Purchase = updatedBook.Purchase;

            await _context.SaveChangesAsync();

            return NoContent(); //not new content returned 
        }

        //// DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(); //404 status code
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
            //204 status code, no content returned 
        }
    }
}
