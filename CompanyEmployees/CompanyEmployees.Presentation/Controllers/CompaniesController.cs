using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public IActionResult GetCompanies()
    {
        return Ok(_serviceManager.CompanyService.GetAllCompanies(false));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCompany(Guid id)
    {
        var company = _serviceManager.CompanyService.GetCompany(id, false);

        return company is null ? NotFound() : Ok(company);
    }
}