using Contracts;
using Contracts.Repositories;
using Entities.Models;
using Service.Contracts;

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

    public IEnumerable<Company> GetAllCompanies(bool trackChanges)
    {
        try
        {
            return _repositoryManager.Company.GetAllCompanies(trackChanges);
        }
        catch(Exception ex)
        {
            _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} method: {ex}");
            throw;
        }
    }
}