using Entities.Models;

namespace Service.Contracts;

public interface ICompanyService
{
    public IEnumerable<Company> GetAllCompanies(bool trackChanges);
}
