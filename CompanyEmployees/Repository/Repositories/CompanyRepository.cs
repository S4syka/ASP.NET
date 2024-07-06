using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories;

internal class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateCompany(Company company) => Create(company);

    public void DeleteCompany(Company company) => Delete(company);

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) 
        => await FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync();

    public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges){
        HashSet<Guid> idsSet = new HashSet<Guid>(ids);

        return await FindByCondition(c => idsSet.Contains(c.Id), trackChanges)
            .ToListAsync();
    }

    public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges)
        => await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefaultAsync();
}