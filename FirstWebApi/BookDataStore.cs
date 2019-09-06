using FirstWebApi.Models;
using FirstWebApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi
{
    public class BookDataStore
    {
      
        public static List<Book> Books { get; set; }

        public static List<Book> GetBooks()
        {
            if (Books == null)
                Books = new List<Book>();
            return Books;
        }
    
        public static Book Get(int id)
        {
            var book = Books.Find(b => b.Id == id);
            if (book == null)
                throw new BookNotFoundException();
            return book;
        }

        public static void Post(Book book)
        {
            
            Books.Add(book);
        }

        public static void Put(int id, Book updatedBook)
        {
            var book = Books.Find(b => b.Id == id);
            if (book == null)
                throw new BookNotFoundException();
            book.CopyPropertiesFrom(updatedBook);
        }

        public static void Delete(int id)
        {
            var book = Books.Find(b => b.Id == id);
            if (book == null)
                throw new BookNotFoundException();
            Books.Remove(book);
        }
    }
}
