using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstWebApi.Models;
using System.Diagnostics;
using FirstWebApi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private List<Book> _books;
        private IBookService _bookService;
         
        public BookController(IBookService bookService)
        {
            // _books.Add(new Book { Id = 1, Name = "CLRS", ISBN = "2001:09", Author = "Cormen" });
            _bookService = bookService;
            _books = _bookService.Get();
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_books);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get(int id)
        {
            Book book = null;
            try
            {
                book = _bookService.Get(id);
            }
            catch (BookNotFoundException)
            {
                return NotFound($"Book with ID {id} not found");
            }
            catch(InvalidIDException)
            {
                return NotFound($"Invalid Id {id}, Id should be a positive number.");
            }
            return Ok(book);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Book input)
        {
            try
            {
                _bookService.Post(input);
            }
            catch (InvalidBookParametersException)
            {
                return NotFound($"Invalid parameters");
            }
            return CreatedAtRoute("GetBook", new { id = input.Id }, input);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book updatedBook)
        {
            try
            {
                _bookService.Put(id, updatedBook);
            }
            catch (InvalidBookParametersException)
            {
                return NotFound($"Invalid parameters");
            }
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
  
            try
            {
                _bookService.Delete(id);
            }
            catch (InvalidBookParametersException)
            {
                return NotFound($"Invalid parameters");
            }
            return NoContent();
        }
    }
}
