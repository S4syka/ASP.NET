using Contracts.Repositories;
using Contracts;
using Service.Contracts;
using AutoMapper;
using Shared.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = loggerManager;
        _mapper = mapper;
    }

    public IEnumerable<EmployeeDTO> GetEmployees(Guid companyId, bool trackChanges)
    {
        var company = _repositoryManager.Company.GetCompany(companyId, trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employeeEntities = _repositoryManager.Employee.GetEmployees(companyId, trackChanges);
        var employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employeeEntities);

        return employeeDTOs;
    }

    public EmployeeDTO GetEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        var company = _repositoryManager.Company.GetCompany(companyId, trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employeeEntity = _repositoryManager.Employee.GetEmployee(companyId, id, trackChanges);
        if(employeeEntity is null)
        {
            throw new EmployeeNotFoundException(id);
        }

        var employeeDTO = _mapper.Map<EmployeeDTO>(employeeEntity);
        return employeeDTO;
    }

    public EmployeeDTO CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDTO employee, bool trackChanges)
    {
        var companyEntity = _repositoryManager.Company.GetCompany(companyId, trackChanges);
        if (companyEntity is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employeeEntity = _mapper.Map<Employee>(employee);

        _repositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        _repositoryManager.Save();

        var employeeDTO = _mapper.Map<EmployeeDTO>(employeeEntity);
        return employeeDTO;
    }

    public void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
    {
        var companyEntity = _repositoryManager.Company.GetCompany(companyId, trackChanges);
        if(companyEntity is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employeeEntity = _repositoryManager.Employee.GetEmployee(companyId, id, trackChanges);
        if (employeeEntity is null)
        {
            throw new EmployeeNotFoundException(id);
        }

        _repositoryManager.Employee.DeleteEmployee(employeeEntity);
        _repositoryManager.Save();
    }

    public void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDTO employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
    {
        var companyEntity = _repositoryManager.Company.GetCompany(companyId, compTrackChanges);
        if (companyEntity is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employeeEntity = _repositoryManager.Employee.GetEmployee(companyId, id, empTrackChanges);
        if (employeeEntity is null)
        {
            throw new EmployeeNotFoundException(id);
        }

        if(employeeForUpdate is null)
        {
            throw new EmployeeForUpdateBadRequestException();
        }

        _mapper.Map(employeeForUpdate, employeeEntity);
        _repositoryManager.Save();
    }
}