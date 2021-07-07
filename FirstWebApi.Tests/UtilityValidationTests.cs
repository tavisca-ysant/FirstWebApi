using FirstWebApi.Models;
using FirstWebApi.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FirstWebApi.Tests
{
    public class UtilityValidationTests
    {
        [Fact]
        public void Valid_attribute_test_book_name_successful()
        {
            var book = new Book { Name = "CLRS" };
            Assert.True(Validation.IsValidAttribute(book.Name));
        }
        [Fact]
        public void Valid_attribute_test_book_name_unsuccessful()
        {
            var book = new Book { Name = "CLRS1" };
            Assert.False(Validation.IsValidAttribute(book.Name));
        }
        [Fact]
        public void Valid_attribute_test_book_category_successful()
        {
            var book = new Book { Category = "Programming" };
            Assert.True(Validation.IsValidAttribute(book.Category));
        }
        [Fact]
        public void Valid_attribute_test_book_category_unsuccessful()
        {
            var book = new Book { Category = "1Programming" };
            Assert.False(Validation.IsValidAttribute(book.Category));
        }
        [Fact]
        public void Valid_attribute_test_book_author_successful()
        {
            var book = new Book { Author = "Cormen" };
            Assert.True(Validation.IsValidAttribute(book.Author));
        }
        [Fact]
        public void Valid_attribute_test_book_author_unsuccessful()
        {
            var book = new Book { Author = "1111" };
            Assert.False(Validation.IsValidAttribute(book.Author));
        }
    }
}
