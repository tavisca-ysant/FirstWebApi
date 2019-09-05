using FirstWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirstWebApi.Utility
{
    public class Validation
    {
        public static bool IsValidAttribute(string Input)
        {
            return Input.All(Char.IsLetter);
        }

        public static bool IsValidBook(Book book)
        {
            
            return   book.Price > 0 &&
                IsValidAttribute(book.Name) && IsValidAttribute(book.Category)
                && IsValidAttribute(book.Author);
        }
    }
}
