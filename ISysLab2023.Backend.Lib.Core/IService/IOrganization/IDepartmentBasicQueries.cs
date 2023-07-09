using ISysLab2023.Backend.Lib.Core.ModelDto.OrganizationDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;

namespace ISysLab2023.Backend.Lib.Core.IService.IOrganization;
public interface IDepartmentBasicQueries
{
    #region BasicQueries
    ICollection<DepartmentDto>? GetDepartments(int page = 1);
    Task<ICollection<DepartmentDto>>? GetDepartmentsAsync(int page = 1);

    DepartmentDto? GetDepartmentByCode(string subdivisionCode);
    Task<DepartmentDto>? GetDepartmentByCodeAsync(string subdivisionCode);

    bool DepartmentExists(string subdivisionCode);
    Task<bool> DepartmentExistsAsync(string subdivisionCode);

    ICollection<EmployeeDto>? GetEmployees(string subdivisionCode, int page = 1);
    Task<ICollection<EmployeeDto>>? GetEmployeesAsync(string subdivisionCode, int page = 1);
    #endregion BasicQueries

}
