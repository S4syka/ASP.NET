using Contracts.Repositories;
using Entities.Models;
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
    public IEnumerable<Company> GetAllCompanies(bool trackChanges) 
        => FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToList();

    public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges){
        HashSet<Guid> idsSet = new HashSet<Guid>(ids);

        return FindByCondition(c => idsSet.Contains(c.Id), trackChanges)
            .ToList();
    }

    public Company? GetCompany(Guid companyId, bool trackChanges)
        => FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefault();
}