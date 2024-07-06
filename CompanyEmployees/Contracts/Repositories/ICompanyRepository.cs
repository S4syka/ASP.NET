using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Models;

namespace Contracts.Repositories;

public interface ICompanyRepository
{
    public Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
    public Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges);
    public Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    public void CreateCompany(Company company);
    public void DeleteCompany(Company company);
}