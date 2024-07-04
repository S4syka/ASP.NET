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
    public IEnumerable<Company> GetAllCompanies(bool trackChanges);
    public Company? GetCompany(Guid companyId, bool trackChanges);
    public void CreateCompany(Company company);
}