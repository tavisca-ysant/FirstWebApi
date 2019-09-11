using FirstWebApi.Models;
using FluentValidation.Results;
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
                if (item.Name.Equals("Price"))
                {
                    if (Int32.Parse(item.GetValue(book).ToString()) <= 0)
                        errorMessages.Add($"Attribute {item.Name} should be a positive Number");
                    continue;
                }

                if (!item.Name.Equals("Id") && !Validation.IsValidAttribute(item.GetValue(book).ToString()))
                    errorMessages.Add($"Attribute {item.Name} is invalid");
            }
            return errorMessages;
        }

        public string ValidateID(int id)
        {
            var errorMsg = "";
            if (id <= 0) errorMsg = $"Invalid ID {id}, should be a positive number";
            return errorMsg;
        }

        public List<string> PopulateMissingBookError(int id)
        {
            var error = new List<string>();
            error.Add($"Book with {id} not found");
            return error;
        }

        public List<string> FluentValidatorErrors(IList<ValidationFailure> errors)
        {
            Debug.WriteLine("Fluent validator called");
            foreach(var error in errors)
            {
                Debug.WriteLine($"Error is {error.ErrorMessage}");
                errorMessages.Add(error.ErrorMessage);
            }
            return errorMessages;
        }
    }
}
