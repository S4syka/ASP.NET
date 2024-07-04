using Contracts.Repositories;
using Contracts;
using Service.Contracts;
using AutoMapper;
using Shared.DataTransferObjects;
using Entities.Exceptions;

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
}