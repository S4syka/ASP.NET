using Entities.Models;
using Shared.DataTransferObjects;


namespace Service.Contracts;

public interface IEmployeeService
{
    public Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(Guid companyId, bool trackChanges);
    public Task<EmployeeDTO> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    public Task<EmployeeDTO> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDTO employee, bool trackChanges);
    public Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges);
    public Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDTO employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
    public Task<(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);
    public Task SaveChangesForPatchAsync(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity);
}
