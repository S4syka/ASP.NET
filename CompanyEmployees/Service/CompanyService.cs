using Contracts;
using Contracts.Repositories;
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
}