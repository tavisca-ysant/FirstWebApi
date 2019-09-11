using FirstWebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static bool ValidateID(int id)
        {
            Debug.WriteLine($"ID in validation is {id}");
            var str = new ErrorHandler().ValidateID(id);
            Debug.WriteLine("Validation msg is " + str);
            return str.Equals("");
        }
        public static bool IsInvalidBook(Book book)
        {
            return new ErrorHandler().BookValidation(book).Count > 0;
        }
    }
}
