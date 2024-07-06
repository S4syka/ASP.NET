using Models.Enums;
using System.ComponentModel.DataAnnotations;
using Models.ValidationAttributes;

namespace Models;

public class Book : IValidatableObject
{
    public string? Name { get; set; }

    [BookGenre(BookGenre.Science)]
    public BookGenre Genre { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errorMessage = $"The genre of the book must be {BookGenre.Science}";
        if (!Genre.Equals(BookGenre.Science.ToString()))
            yield return new ValidationResult(errorMessage, new[] {nameof(Genre) });
    }
}
