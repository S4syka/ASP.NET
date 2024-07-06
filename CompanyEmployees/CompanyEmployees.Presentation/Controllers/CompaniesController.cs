using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using CompanyEmployees.Presentation.ModelBinders;
using Entities.Models;

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
    public async Task<IActionResult> GetCompanies() => Ok(await _serviceManager.CompanyService.GetAllCompaniesAsync(false));

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(Guid id) => Ok(await _serviceManager.CompanyService.GetCompanyAsync(id, false));

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable <Guid> ids) => Ok(await _serviceManager.CompanyService.GetByIdsAsync(ids, false));

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDTO company)
    {
        if (company is null)
        {
            return BadRequest("CompanyForCreationDTO object is null");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var createdCompany = await _serviceManager.CompanyService.CreateCompanyAsync(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }

    [HttpPost("Collection")]
    public async Task<IActionResult> CreateCompanyCollecition([FromBody] IEnumerable<CompanyForCreationDTO> companyCollection)
    {
        var (companyDTOs, ids) = await _serviceManager.CompanyService.CreateCompanyCollectionAsync(companyCollection);

        return CreatedAtRoute("CompanyCollection", new { ids }, companyDTOs);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _serviceManager.CompanyService.DeleteCompanyAsync(id, false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDTO company)
    {
        if (company is null)
        {
            return BadRequest("CompanyForUpdateDto object is null");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _serviceManager.CompanyService.UpdateCompanyAsync(id, company, false);
        return NoContent();
    }
}