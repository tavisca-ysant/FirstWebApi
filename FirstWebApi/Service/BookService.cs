using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWebApi.Models;
using FirstWebApi.Utility;

namespace FirstWebApi.Service
{
    public class BookService : IBookService
    {
        public void Delete(int id)
        {
            if (id <= 0)
                throw new InvalidIDException();
            BookDataStore.Delete(id);
        }
        public List<Book> Get()
        {
            return BookDataStore.GetBooks();
        }
        public Book Get(int id)
        {
            if (id <= 0)
                throw new InvalidIDException();
            return BookDataStore.Get(id);
        }

        public void Post(Book book)
        {
            if (!Validation.IsValidBook(book))
                throw new InvalidBookParametersException();
            if (book.Id <= 0)
                throw new InvalidIDException();
            BookDataStore.Post(book);
        }

        public void Put(int id, Book updatedBook)
        {
            if (!Validation.IsValidBook(updatedBook))
                throw new InvalidBookParametersException();
            
            BookDataStore.Put(id, updatedBook);
        }
    }


}
