using AutoMapper;
using Contracts;
using Contracts.Repositories;
using Entities.Models;
using Service.Contracts;
using Entities.Exceptions;
using Shared.DataTransferObjects;

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

    public CompanyDTO CreateCompany(CompanyForCreationDTO company)
    {
        var companyEntity = _mapper.Map<Company>(company);

        _repositoryManager.Company.CreateCompany(companyEntity);
        _repositoryManager.Save();

        var companyDTO = _mapper.Map<CompanyDTO>(companyEntity);
        return companyDTO;
    }

    public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
    {
        var companyEntities = _repositoryManager.Company.GetAllCompanies(trackChanges);
        var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);

        return companyDTOs;
    }

    public IEnumerable<CompanyDTO> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if(ids is null)
        {
            throw new IdParametersBadRequestException();
        }

        var companyEntities = _repositoryManager.Company.GetByIds(ids, trackChanges);
        if(companyEntities is null || companyEntities.Count() != ids.Count())
        {
            throw new CollectionByIdsBadRequestException();
        }

        var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);
        return companyDTOs;
    }

    public CompanyDTO GetCompany(Guid companyId, bool trackChanges)
    {
        var companyEntity = _repositoryManager.Company.GetCompany(companyId, trackChanges);
        if(companyEntity is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var companyDTO = _mapper.Map<CompanyDTO>(companyEntity);

        return companyDTO;
    }
}