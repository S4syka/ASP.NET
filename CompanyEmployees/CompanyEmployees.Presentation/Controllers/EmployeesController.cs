using Entities.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId:guid}/employees")]
[ApiController]
public class EmployeesController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public EmployeesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesForCompany(Guid companyId) => Ok(await _serviceManager.EmployeeService.GetEmployeesAsync(companyId, false));

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id) => Ok(await _serviceManager.EmployeeService.GetEmployeeAsync(companyId, id, false));

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDTO employee)
    { 
        var employeeDTO = await _serviceManager.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, false);
        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeDTO.id }, employeeDTO);
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDTO employee)
    {
        await _serviceManager.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee, false, true);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDTO> patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest("patchDoc object sent from client is null.");
        }

        var result = await _serviceManager.EmployeeService.GetEmployeeForPatchAsync(companyId, id, false, true);

        patchDoc.ApplyTo(result.employeeToPatch, ModelState);
        TryValidateModel(result.employeeToPatch);

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _serviceManager.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);

        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        await _serviceManager.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, false);
        return NoContent();
    }
}