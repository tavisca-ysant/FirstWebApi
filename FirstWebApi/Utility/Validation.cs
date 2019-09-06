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

        public static bool IsInvalidBook(Book book)
        {
            return new ErrorHandler().BookValidation(book).Count > 0;
        }
    }
}
