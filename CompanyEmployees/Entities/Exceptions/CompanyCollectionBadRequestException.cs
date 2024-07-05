namespace Entities.Exceptions;

public class CompanyCollectionBadRequestException : BadRequestException
{
    public CompanyCollectionBadRequestException() : base("Company collection sent from a client is a null")
    {
    }
}