using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDTO>().ForMember("FullAddress", opt => opt.MapFrom(x => string.Join(" ", x.Address, x.Country)));
        CreateMap<CompanyForCreationDTO, Company>();
        CreateMap<Employee, EmployeeDTO>();
        CreateMap<EmployeeForCreationDTO, Employee>();
        CreateMap<EmployeeForUpdateDTO, Employee>();
    }
}
