using FirstWebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi.Utility
{
    public class ErrorHandler
    {
        
        private List<string> errorMessages = new List<string>();
        public List<string> BookValidation(Book book)
        {
            var propertyList = book.GetType().GetProperties();
            foreach (var item in propertyList)
            {
                if (item.Name.Equals("Price") || item.Name.Equals("Id"))
                {
                    if (Int32.Parse(item.GetValue(book).ToString()) <= 0)
                        errorMessages.Add($"Attribute {item.Name} should be a positive Number");
                    continue;
                }

                if (!Validation.IsValidAttribute(item.GetValue(book).ToString()))
                    errorMessages.Add($"Attribute {item.Name} is invalid");
            }
            return errorMessages;
        }
    }
}
