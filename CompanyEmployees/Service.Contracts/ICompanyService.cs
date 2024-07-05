using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges);
    public CompanyDTO GetCompany(Guid companyId, bool trackChanges);
    public IEnumerable<CompanyDTO> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    public CompanyDTO CreateCompany(CompanyForCreationDTO company);
    public void DeleteCompany(Guid companyId, bool trackChanges);
    public (IEnumerable<CompanyDTO>, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDTO> companyCollection);
    public void UpdateCompany(Guid companyId, CompanyForUpdateDTO companyForUpdate, bool trackChanges);
}