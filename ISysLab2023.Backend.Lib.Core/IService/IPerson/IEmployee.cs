using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;

namespace ISysLab2023.Backend.Lib.Core.IService.IPerson;

/// <summary>
/// Implements the Repository pattern, 
/// presents an interface for the Employee class
/// </summary>
public interface IEmployee
{

    #region idontknow
    Department GetIdDepartment(string departmentCode);
    #endregion idontknow

    #region BasicQueries
    ICollection<Employee>? GetEmployees();
    Task<ICollection<Employee>>? GetEmployeesAsync();

    Employee? GetEmployeeByCode(int employeeCode);
    Task<Employee>? GetEmployeeByCodeAsync(int employeeCode);

    ICollection<Project>? GetProject(int employeeCode);
    Task<ICollection<Project>>? GetProjectsAsync(int employeeCode);

    bool EmployeeExists(int employeeCode);
    Task<bool> EmployeeExistsAsync(int employeeCode);

    bool ParticipatesInProject(string projectCode, int employeeCode);
    Task<bool> ParticipatesInProjectAsync(string projectCode, int employeeCode);

    ICollection<Employee>? GetSubEmployees(int employeeCode);
    Task<ICollection<Employee>>? GetSubEmployeesAsync(int employeeCode);

    #endregion BasicQueries

    #region CRUD

    bool CreateEmployee(Employee employee);
    bool UpdateEmployee(Employee employee);
    bool DeleteEmployee(int employeeCode);
    bool Save();

    Task<bool> CreateEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(int employeeCode);
    Task<bool> SaveAsync();

    #endregion CRUD
}
