using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    public Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync(bool trackChanges);
    public Task<CompanyDTO> GetCompanyAsync(Guid companyId, bool trackChanges);
    public Task<IEnumerable<CompanyDTO>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    public Task<CompanyDTO> CreateCompanyAsync(CompanyForCreationDTO company);
    public Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
    public Task<(IEnumerable<CompanyDTO>, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDTO> companyCollection);
    public Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDTO companyForUpdate, bool trackChanges);
}