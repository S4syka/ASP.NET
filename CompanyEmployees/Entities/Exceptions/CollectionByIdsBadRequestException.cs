namespace Entities.Exceptions;

public class CollectionByIdsBadRequestException : BadRequestException
{
    public CollectionByIdsBadRequestException() : base("Collection size mismatch comparing to ids.")
    {
    }
}
