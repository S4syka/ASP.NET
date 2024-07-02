﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Repositories;
using Repository.Repositories;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICompanyRepository> _companyRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;

    public RepositoryManager(RepositoryContext context)
    {
        _repositoryContext = context;
        _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(context));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
    }

    public ICompanyRepository Company => _companyRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();
}