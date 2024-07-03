using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges);
}
