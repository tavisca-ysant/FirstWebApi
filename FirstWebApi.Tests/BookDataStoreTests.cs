using FirstWebApi.Models;
using System;
using Xunit;

namespace FirstWebApi.Tests
{
    public class BookDataStoreTests
    {
        [Fact]
        public void Empty_Data_Store_test()
        {
            var bookList = BookDataStore.GetBooks();
            Assert.Empty(bookList);
        }

        [Fact]
        public void Check_book_data_store_after_adding_book()
        {
            var bookList = BookDataStore.GetBooks();
            bookList.Add(new Models.Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "G Sanap",
                Price = 1000
            });
            Assert.Single(bookList);
        }

        [Fact]
        public void Update_book_data_using_put_method_success_scenario()
        {
            var bookList = BookDataStore.GetBooks();
            bookList.Add(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "GSanap",
                Price = 1000
            });
            var newBook = new Book
            {
                Name = "Java",
                Author = "Sanap",
                Category = "Program",
                Price = 500
            };
            BookDataStore.Put(1, newBook);
            var updatedBook = BookDataStore.Get(1);
            Assert.Equal(newBook.Name, updatedBook.Name);
        }
        [Fact]
        public void Update_book_data_using_put_method_failure_scenario()
        {
            var bookList = BookDataStore.GetBooks();
            bookList.Add(new Models.Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "G Sanap",
                Price = 1000
            });
            var newBook = new Book
            {
                Name = "Java",
                Author = "G Sanap"
            };
            Assert.Throws<BookNotFoundException>(() => BookDataStore.Put(2, newBook));
           
        }
        [Fact]
        public void Delete_book_using_put_method_success_scenario()
        {
            var bookList = BookDataStore.GetBooks();
            bookList.Add(new Models.Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "G Sanap",
                Price = 1000
            });

            BookDataStore.Delete(1);
            Assert.Empty(bookList);
        }
        [Fact]
        public void Delete_book_using_put_method_failure_scenario()
        {
            var bookList = BookDataStore.GetBooks();
            bookList.Add(new Models.Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "G Sanap",
                Price = 1000
            });
            
            Assert.Throws<BookNotFoundException>(() => BookDataStore.Delete(2));

        }
    }
}
