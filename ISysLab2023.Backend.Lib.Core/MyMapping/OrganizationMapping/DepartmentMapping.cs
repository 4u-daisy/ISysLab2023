using ISysLab2023.Backend.Lib.Core.ModelDto.OrganizationDto;
using ISysLab2023.Backend.Lib.Domain.Organization;

namespace ISysLab2023.Backend.Lib.Core.MyMapping.OrganizationMapping;
public static class DepartmentMapping
{
    public static Department? Mapping(DepartmentDto? departmentDto)
    {
        return departmentDto == null ? null : new Department()
        {
            Id = departmentDto.Id,
            Title = departmentDto.Title,
            SubdivisionCode = departmentDto.SubdivisionCode,
        };
    }
    public static DepartmentDto? Mapping(Department? department)
    {
        return department == null ? null : new DepartmentDto()
        {
            Id = department.Id,
            Title = department.Title,
            SubdivisionCode = department.SubdivisionCode,
        };
    }

    public static IEnumerable<Department>? Mapping
        (IEnumerable<DepartmentDto>? departmentDto) =>
        departmentDto == null ? null : departmentDto.Select(x => Mapping(x));

    public static IEnumerable<DepartmentDto>? Mapping
        (IEnumerable<Department>? department) =>
        department == null ? null : department.Select(x => Mapping(x));

}
