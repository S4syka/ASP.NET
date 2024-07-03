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

    public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _logger = loggerManager;
    }

    public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
    {
        try
        {
            var companyEntities = _repositoryManager.Company.GetAllCompanies(trackChanges);
            var companyDTOs = companyEntities.Select(c => new CompanyDTO(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country))).ToList();

            return companyDTOs;
        }
        catch(Exception ex)
        {
            _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} method: {ex}");
            throw;
        }
    }
}