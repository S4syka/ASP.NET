﻿using Contracts.Repositories;
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

    public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(Guid companyId, bool trackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, trackChanges);

        var employeeEntities = await _repositoryManager.Employee.GetEmployeesAsync(companyId, trackChanges);
        var employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employeeEntities);

        return employeeDTOs;
    }

    public async Task<EmployeeDTO> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, trackChanges);
        var employeeEntity = await GetEmployeeEntity(companyId, id, trackChanges);

        var employeeDTO = _mapper.Map<EmployeeDTO>(employeeEntity);
        return employeeDTO;
    }

    public async Task<EmployeeDTO> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDTO employee, bool trackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, trackChanges);
        var employeeEntity = _mapper.Map<Employee>(employee);

        _repositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        await _repositoryManager.SaveAsync();

        var employeeDTO = _mapper.Map<EmployeeDTO>(employeeEntity);
        return employeeDTO;
    }

    public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, trackChanges);
        var employeeEntity = await GetEmployeeEntity(companyId, id, trackChanges);

        _repositoryManager.Employee.DeleteEmployee(employeeEntity);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDTO employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, compTrackChanges);
        var employeeEntity = await GetEmployeeEntity(companyId, id, empTrackChanges);

        if (employeeForUpdate is null)
        {
            throw new EmployeeForUpdateBadRequestException();
        }

        _mapper.Map(employeeForUpdate, employeeEntity);
        await _repositoryManager.SaveAsync();
    }

    public async Task<(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, compTrackChanges);
        var employeeEntity = await GetEmployeeEntity(companyId, id, empTrackChanges);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDTO>(employeeEntity);
        return (employeeToPatch, employeeEntity);
    }

    public async Task SaveChangesForPatchAsync(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        await _repositoryManager.SaveAsync();
    }

    private async Task<Company> GetCompanyEntity(Guid companyId, bool trackChanges)
    {
        var companyEntity = await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
        if (companyEntity is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        return companyEntity;
    }

    private async Task<Employee> GetEmployeeEntity(Guid companyId, Guid id, bool empTrackChanges)
    {
        var employeeEntity = await _repositoryManager.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
        if (employeeEntity is null)
        {
            throw new EmployeeNotFoundException(id);
        }

        return employeeEntity;
    }
}