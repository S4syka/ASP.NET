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
        try
        {
            var companyEntities = _repositoryManager.Company.GetAllCompanies(trackChanges);
            var companyDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);

            return companyDTOs;
        }
        catch(Exception ex)
        {
            _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} method: {ex}");
            throw;
        }
    }
}