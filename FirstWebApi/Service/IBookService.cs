using FirstWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi.Service
{
    public interface IBookService
    {
        List<Book> Get();
        Book Get(int id);
        void Post(Book book);
        void Put(int id, Book updatedBook);
        void Delete(int id);
    }
}
