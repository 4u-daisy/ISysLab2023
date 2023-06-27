using AutoMapper;
using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using ISysLab2023.Backend.WebApi.ModelsDto.OrganizationDto;
using ISysLab2023.Backend.WebApi.ModelsDto.PersonDto;
using ISysLab2023.Backend.WebApi.ModelsDto.WorkingProjectDto;

namespace ISysLab2023.Backend.WebApi.Service.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentDto, Department>();

        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();

        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();
    }
}
