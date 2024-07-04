using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CompaniesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetCompanies() => Ok(_serviceManager.CompanyService.GetAllCompanies(false));

    [HttpGet("{id:guid}")]
    public IActionResult GetCompany(Guid id) => Ok(_serviceManager.CompanyService.GetCompany(id, false));
}