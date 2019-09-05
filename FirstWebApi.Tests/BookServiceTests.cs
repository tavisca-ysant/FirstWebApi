using FirstWebApi.Models;
using FirstWebApi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FirstWebApi.Tests
{
    public class BookServiceTests
    {
        private IBookService bookService = new BookService();
        [Fact]
        public void Empty_Data_Store_test()
        {
            var bookList = bookService.Get();
            Assert.Empty(bookList);
        }

        [Fact]
        public void Try_getting_book_with_invalid_ID()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });
            Assert.Throws<InvalidIDException>(() => bookService.Get(-1));
        }
        
        [Fact]
        public void Insert_book_using_post_method_invalid_name()
        {
            var bookList = bookService.Get();

            Assert.Throws<InvalidBookParametersException>(() => bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet ",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            }));
        }
        [Fact]
        public void Insert_book_using_post_method_invalid_category()
        {
            var bookList = bookService.Get();
            Assert.Throws<InvalidBookParametersException>(() => bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming11",
                Author = "Sanap",
                Price = 1000
            }));
        }
        [Fact]
        public void Insert_book_using_post_method_invalid_author()
        {
            var bookList = bookService.Get();
            Assert.Throws<InvalidBookParametersException>(() => bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "...Sanap",
                Price = 1000
            }));
        }
        [Fact]
        public void Insert_book_using_post_method_invalid_id()
        {
            var bookList = bookService.Get();
            Assert.Throws<InvalidIDException>(() => bookService.Post(new Book
            {
                Id = -1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            }));
        }
        [Fact]
        public void Insert_book_using_post_method_invalid_price()
        {
            var bookList = bookService.Get();
            Assert.Throws<InvalidBookParametersException>(() => bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = -1000
            }));
        }
        [Fact]
        public void Update_book_using_put_method_valid_attributes()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });
            bookService.Put(1, new Book
            {
                Name = "Java",
                Category = "ProgrammingNew",
                Author = "GSanap",
                Price = 400
            });
            var updatedBook = bookService.Get(1);
            Assert.Equal("Java", updatedBook.Name);
        }
        [Fact]
        public void Update_book_using_put_method_invalid_name()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });
            Assert.Throws<InvalidBookParametersException>(() => bookService.Put(1, new Book
            {
                Name = "Dotnet ",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            }));
        }
        [Fact]
        public void Update_book_using_put_method_invalid_category()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });
            Assert.Throws<InvalidBookParametersException>(() => bookService.Put(1, new Book
            {
                Name = "DotnetNew",
                Category = "Programming11",
                Author = "Sanap",
                Price = 1000
            }));
        }
        [Fact]
        public void Update_book_using_put_method_invalid_author()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });
            Assert.Throws<InvalidBookParametersException>(() => bookService.Put(1, new Book
            {
                Name = "DotnetNew",
                Category = "Program",
                Author = "Sanap....",
                Price = 1000
            }));
        }
        [Fact]
        public void Update_book_using_put_method_invalid_price()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });
            Assert.Throws<InvalidBookParametersException>(() => bookService.Put(1, new Book
            {
                Name = "DotnetDotnet",
                Category = "Program",
                Author = "GSanap",
                Price = -1000
            }));
        }
        [Fact]
        public void Insert_book_using_post_method_valid_attributes()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });
            Assert.Single(bookList);
        }
        [Fact]
        public void Delete_book_method_success_scenario()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });

            bookService.Delete(1);
            Assert.Empty(bookList);
        }
        [Fact]
        public void Delete_book_failure_scenario()
        {
            var bookList = bookService.Get();
            bookService.Post(new Book
            {
                Id = 1,
                Name = "Dotnet",
                Category = "Programming",
                Author = "Sanap",
                Price = 1000
            });

            Assert.Throws<BookNotFoundException>(() => bookService.Delete(2));

        }
    }
}
