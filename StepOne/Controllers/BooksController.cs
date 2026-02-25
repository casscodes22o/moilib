using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Le Grand Meaulnus",
                Author = "Alain Henri",
                Purchase = Bought.ONLINE
            },
            new Book
            {
                Id = 2,
                Title = "The Summer of Katya",
                Author = "Trevanian",
                Purchase = Bought.PSHOP,
            },
            new Book
            {
                Id = 3,
                Title = "A Song of Ice and Fire: A Game of Thrones",
                Author = "George R.R. Martin",
                Purchase = Bought.BNEW
            },
            new Book
            {
                Id = 4,
                Title = "Terrible Tudors",
                Author = "Terry Deary",
                Purchase = Bought.PSHOP
            }
        };

        // GET retrieves information 
        // ActionResul returns status code and result of request 
        // ActionResult {stuffToReturn} {methodName}
        [HttpGet]
        public ActionResult<List<Book>> getBookList()
        {
            return Ok(books); //Ok gies 200 status code
        }

        // GATES this parameter next to Httpget spawns what to put next to "book"/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> getBookById(int id)
        {
            // FirstOrDefault for 
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); //404 status code

            return Ok(book); //200 status code
        }
        //KEY Learning (?) you preempt the status code; i.e. here use of 
        //Ok() and NotFound() API does not decide on its own(?) 
        //HENCE, look into these status code class  

        //POST user send info 
        [HttpPost]
        public ActionResult<Book> addBook(Book newBook)
        {
            bool bookExists = books.Any(b => b.Id == newBook.Id);

            if (newBook == null)
                return BadRequest();
            else if (bookExists)
                Conflict($"Book with this ID = {newBook.Id} already exists :<");
            else
                books.Add(newBook);
            return CreatedAtAction(nameof(getBookById), new { id = newBook.Id }, newBook);

            // always return Ok() when req is succesful 
            // I dont want to tackle other data structs in C# yet 
            // easy dupe id fix is check if already exists then send bad request 
        }

        //PUT
        // mind the parameters, update only works if the id is included in URL 
        [HttpPut("{id}")]
        public IActionResult updateBook(int id, Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); //404 status code

            // book.Id = updatedBook.Id; REMOVE ID  bc u already have it
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Purchase = updatedBook.Purchase;

            return NoContent(); //not new content returned 
        }

        // DELETE 
        [HttpDelete("{id}")]
        public IActionResult deleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); //404 status code
            books.Remove(book);
            return NoContent();
            //204 status code, no content returned 
        }
    }
}
