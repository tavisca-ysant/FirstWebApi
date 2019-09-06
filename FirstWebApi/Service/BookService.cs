using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWebApi.Models;
using FirstWebApi.Utility;
using System.Diagnostics;

namespace FirstWebApi.Service
{
    public class BookService : IBookService
    {
        public static int ID = 0;
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
            book.Id = ++ID;
            if (Validation.IsInvalidBook(book))
                throw new InvalidBookParametersException();
            BookDataStore.Post(book);
        }

        public void Put(int id, Book updatedBook)
        {
            updatedBook.Id = id;
            
            if (Validation.IsInvalidBook(updatedBook))
                throw new InvalidBookParametersException();
            
            BookDataStore.Put(id, updatedBook);
        }
    }


}
