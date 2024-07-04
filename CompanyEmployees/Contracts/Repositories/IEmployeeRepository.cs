using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories;

public interface IEmployeeRepository
{
    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
    public Employee? GetEmployee(Guid companyId, Guid id, bool trackChanges);
    public void CreateEmployeeForCompany(Guid companyId, Employee employee);
}