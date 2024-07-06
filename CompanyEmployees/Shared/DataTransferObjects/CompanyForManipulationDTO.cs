﻿using Shared.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

public abstract record CompanyForManipulationDTO
{
    [Required(ErrorMessage = "Company name is a required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for company name is 60 characters")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Company address is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
    public string? Address { get; init; }
    public string? Country { get; init; }
    public IEnumerable<EmployeeForCreationDTO>? Employees { get; init; }
}