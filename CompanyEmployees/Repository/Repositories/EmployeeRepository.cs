using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        => await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
        .SingleOrDefaultAsync();

    public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters parameters, bool trackChanges)
    {
        var employees =  await FindByCondition(e => e.CompanyId.Equals(companyId) && (e.Age >= parameters.MinAge && e.Age <= parameters.MaxAge), trackChanges)
        .OrderBy(e => e.Name)
        .Skip((parameters.PageNumber - 1) * parameters.PageSize)
        .Take(parameters.PageSize)
        .ToListAsync();

        var employeesCount = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).CountAsync();

        return new PagedList<Employee>(employees, employeesCount, parameters.PageNumber, parameters.PageSize);
    }

    public void DeleteEmployee(Employee employee) => Delete(employee);
    
    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }
}