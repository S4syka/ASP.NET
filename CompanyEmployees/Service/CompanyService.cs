using AutoMapper;
using Contracts;
using Contracts.Repositories;
using Entities.Models;
using Service.Contracts;
using Entities.Exceptions;
using Shared.DataTransferObjects;
using System.ComponentModel.Design;

namespace Service;

internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = loggerManager;
        _mapper = mapper;
    }

    public async Task<CompanyDTO> CreateCompanyAsync(CompanyForCreationDTO company)
    {
        var companyEntity = _mapper.Map<Company>(company);

        _repositoryManager.Company.CreateCompany(companyEntity);
        await _repositoryManager.SaveAsync();

        var companyDTO = _mapper.Map<CompanyDTO>(companyEntity);
        return companyDTO;
    }

    public async Task<(IEnumerable<CompanyDTO>, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDTO> companyCollection)
    {
        if(companyCollection == null)
        {
            throw new CompanyCollectionBadRequestException();
        }

        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

        foreach (var companyEntity in companyEntities)
        {
            _repositoryManager.Company.CreateCompany(companyEntity);
        }
        await _repositoryManager.SaveAsync();

        var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);
        string ids = string.Join(",", companyDTOs.Select(c => c.Id));
        return (companyDTOs, ids);
    }

    public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, trackChanges);

        _repositoryManager.Company.DeleteCompany(companyEntity);
        await _repositoryManager.SaveAsync();
    }

    public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync(bool trackChanges)
    {
        var companyEntities = await _repositoryManager.Company.GetAllCompaniesAsync(trackChanges);
        var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);

        return companyDTOs;
    }

    public async Task<IEnumerable<CompanyDTO>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if(ids is null)
        {
            throw new IdParametersBadRequestException();
        }

        var companyEntities = await _repositoryManager.Company.GetByIdsAsync(ids, trackChanges);
        if(companyEntities is null || companyEntities.Count() != ids.Count())
        {
            throw new CollectionByIdsBadRequestException();
        }

        var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);
        return companyDTOs;
    }

    public async Task<CompanyDTO> GetCompanyAsync(Guid companyId, bool trackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, trackChanges);
        var companyDTO = _mapper.Map<CompanyDTO>(companyEntity);

        return companyDTO;
    }

    public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDTO companyForUpdate, bool trackChanges)
    {
        var companyEntity = await GetCompanyEntity(companyId, trackChanges);

        _mapper.Map(companyForUpdate, companyEntity);
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
}