using Entities.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
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
    public IActionResult GetEmployeesForCompany(Guid companyId) => Ok(_serviceManager.EmployeeService.GetEmployees(companyId, false));

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id) => Ok(_serviceManager.EmployeeService.GetEmployee(companyId, id, false));

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDTO employee)
    { 
        if(employee is null)
        {
            return BadRequest("EmployeeForCreationDTO object is null");
        }

        var employeeDTO = _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employee, false);

        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeDTO.id }, employeeDTO);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult  UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDTO employee)
    {
        _serviceManager.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee, false, true);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public IActionResult PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
[FromBody] JsonPatchDocument<EmployeeForUpdateDTO> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("patchDoc object sent from client is null.");
        var result = _serviceManager.EmployeeService.GetEmployeeForPatch(companyId, id,
        compTrackChanges: false,
        empTrackChanges: true);
        patchDoc.ApplyTo(result.employeeToPatch);
        _serviceManager.EmployeeService.SaveChangesForPatch(result.employeeToPatch,
        result.employeeEntity);
        return NoContent();
    }
}