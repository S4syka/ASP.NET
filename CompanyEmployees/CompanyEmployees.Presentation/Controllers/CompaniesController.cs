using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public IActionResult GetCompany(Guid id) => Ok(_serviceManager.CompanyService.GetCompany(id, false));

    [HttpGet("Collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection(IEnumerable<Guid> ids) => Ok(_serviceManager.CompanyService.GetByIds(ids, false));

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyForCreationDTO company)
    {
        if (company is null)
        {
            return BadRequest("CompanyForCreationDTO object is null");
        }

        var createdCompany = _serviceManager.CompanyService.CreateCompany(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }
}