using FirstWebApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi.Utility
{
    public class BookFluentValidator : AbstractValidator<Book>
    {
        public BookFluentValidator()
        {
            RuleFor(book => book.Name).Must(ValidStrings).WithMessage($"Attribute Name is invalid");
            RuleFor(book => book.Category).Must(ValidStrings).WithMessage($"Attribute Category is invalid");
            RuleFor(book => book.Author).Must(ValidStrings).WithMessage($"Attribute Author is invalid");
            RuleFor(book => book.Id).GreaterThan(0).WithMessage($"ID should be a positive number");
            RuleFor(book => book.Price).GreaterThan(0).WithMessage($"Price should be a positive number");
        }

        private bool ValidStrings(string input)
        {
            return input.All(Char.IsLetter);
        }
    }
}
