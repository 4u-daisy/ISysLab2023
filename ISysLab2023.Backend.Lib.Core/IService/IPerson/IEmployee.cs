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
    // TO DO add pagination

    ICollection<Employee>? GetEmployees();
    Task<ICollection<Employee>>? GetEmployeesAsync();

    Employee? GetEmployeeByCode(int codeEmployee);
    Task<Employee>? GetEmployeeByCodeAsync(int codeEmployee);

    ICollection<Project>? GetProject(int codeEmployee);
    Task<ICollection<Project>>? GetProjectsAsync(int codeEmployee);

    bool ParticipatesInProject(string projectCode, int codeEmployee);
    Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee);

    bool EmployeeExists(int codeEmployee);
    Task<bool> EmployeeExistsAsync(int codeEmployee);

    ICollection<Employee>? GetSubEmployees(int codeEmployee);
    Task<ICollection<Employee>>? GetSubEmployeesAsync(int codeEmployee);

    #region CRUD

    bool CreateEmployee(Employee employee);
    bool UpdateEmployee(Employee employee);
    bool DeleteEmployee(int codeEmployee);
    bool Save();

    Task<bool> CreateEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(int codeEmployee);
    Task<bool> SaveAsync();

    #endregion CRUD
}
