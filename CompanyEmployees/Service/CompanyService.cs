using AutoMapper;
using Contracts;
using Contracts.Repositories;
using Entities.Models;
using Service.Contracts;
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

    public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
    {
        var companyEntities = _repositoryManager.Company.GetAllCompanies(trackChanges);
        var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);

        return companyDTOs;
    }
}