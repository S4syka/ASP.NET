using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ValidationAttributes;

public class BookGenreAttribute(BookGenre genre) : ValidationAttribute
{
    public BookGenre Genre { get; init; } = genre;
    public string Error => $"The genre of the book must be {Genre}";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var book = (Book)validationContext.ObjectInstance;
        if(book.Genre != Genre)
        {
            return new ValidationResult(Error);
        }

        return ValidationResult.Success;
    }
}
