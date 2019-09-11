using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstWebApi.Models;
using System.Diagnostics;
using FirstWebApi.Service;
using FirstWebApi.Utility;
using Microsoft.AspNetCore.JsonPatch;
using NLog;

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
            _bookService = bookService;
            _books = _bookService.Get();
            _response = new Response();
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            //_logger.LogInformation($"Book list fetched");
            return Ok(_books);
        }

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

        [HttpPost]
        public IActionResult Post([FromBody]Book input)
        {
            try
            {
                Debug.WriteLine("COntroller " + input.Name);
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
            return Created("http://localhost:54471/api/book/", _response.Book);
            // return Ok(input);
        }

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

        [HttpPatch("{id}")]
        public IActionResult Patch(int id,
                            [FromBody] JsonPatchDocument<Book> patchedBook)
        {
            if (patchedBook == null)
                return BadRequest();
            var bookFromStore = _bookService.Get(id);
            var newBookObj = new Book()
            {
                Name = bookFromStore.Name
            };
            patchedBook.ApplyTo(newBookObj, ModelState);
            bookFromStore.Name = newBookObj.Name;
            Debug.WriteLine($"Updated book name is {bookFromStore.Name}");
            Debug.WriteLine($"Updated book category is {newBookObj.Category}");
            return Ok();
        }
    }
}
