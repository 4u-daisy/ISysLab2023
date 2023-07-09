using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;

namespace ISysLab2023.Backend.Lib.Core.IService.IPerson;
public interface IEmployeeBasicQueries
{
    #region BasicQueries
    ICollection<EmployeeDto>? GetEmployees(int page = 1);
    Task<ICollection<EmployeeDto>>? GetEmployeesAsync(int page = 1);

    EmployeeDto? GetEmployeeByCode(int employeeCode);
    Task<EmployeeDto>? GetEmployeeByCodeAsync(int employeeCode);

    ICollection<ProjectDto>? GetProject(int employeeCode, int page = 1);
    Task<ICollection<ProjectDto>>? GetProjectsAsync(int employeeCode, int page = 1);

    bool EmployeeExists(int employeeCode);
    Task<bool> EmployeeExistsAsync(int employeeCode);

    bool ParticipatesInProject(string projectCode, int employeeCode);
    Task<bool> ParticipatesInProjectAsync(string projectCode, int employeeCode);

    ICollection<EmployeeDto>? GetSubEmployees(int employeeCode, int page = 1);
    Task<ICollection<EmployeeDto>>? GetSubEmployeesAsync(int employeeCode, int page = 1);

    #endregion BasicQueries
}
