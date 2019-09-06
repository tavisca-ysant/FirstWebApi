using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstWebApi.Models;
using System.Diagnostics;
using FirstWebApi.Service;
using FirstWebApi.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private List<Book> _books;
        private IBookService _bookService;
        private Response _response;
        
        public BookController(IBookService bookService)
        {
            // _books.Add(new Book { Id = 1, Name = "CLRS", ISBN = "2001:09", Author = "Cormen" });
            _bookService = bookService;
            _books = _bookService.Get();
            _response = new Response();
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
            catch (Exception ex)
            {
                if (ex is InvalidIDException)
                    return BadRequest($"Invalid ID {id}, should be a positive number");
                if (ex is BookNotFoundException)
                    return NotFound($"Book with ID {id} not found");
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
            catch (Exception ex)
            {
                _response.Error = new ErrorHandler().BookValidation(input);
                if (ex is InvalidBookParametersException)
                    return BadRequest(_response.Error);
                return StatusCode(500);
            }
            _response.Book = input;
            return Created("http://localhost:54471/api/book/",_response.Book);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book updatedBook)
        {
            try
            {
                _bookService.Put(id, updatedBook);
            }
            catch (Exception ex)
            {
                _response.Error = new ErrorHandler().BookValidation(updatedBook);
                if (ex is InvalidBookParametersException)
                    return BadRequest(_response.Error);
                return StatusCode(500);
            }
            _response.Book = _bookService.Get(id);
            return Ok(_response.Book);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _bookService.Delete(id);
            }
            catch (Exception ex)
            {
                if (ex is InvalidIDException)
                    return BadRequest($"Invalid ID {id}, should be a positive number");
                if (ex is BookNotFoundException)
                    return NotFound($"Book with ID {id} not found");
            }
            return Ok($"Book with ID {id} deleted successfully");
        }
    }
}
