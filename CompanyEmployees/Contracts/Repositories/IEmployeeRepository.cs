using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestFeatures;

namespace Contracts.Repositories;

public interface IEmployeeRepository
{
    public Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters parameters, bool trackChanges);
    public Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    public void CreateEmployeeForCompany(Guid companyId, Employee employee);
    public void DeleteEmployee(Employee employee);
}