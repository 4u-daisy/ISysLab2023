using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.Lib.Domain.Person;

namespace ISysLab2023.Backend.Lib.Core.IService.IOrganization;

/// <summary>
/// Implements the Repository pattern, 
/// presents an interface for the Department class
/// </summary>
public interface IDepartment
{
    // TO DO add pagination

    ICollection<Department>? GetDepartments();
    Task<ICollection<Department>>? GetDepartmentsAsync();

    Department? GetDepartmentByCode(string subdivisionCode);
    Task<Department>? GetDepartmentByCodeAsync(string subdivisionCode);

    bool DepartmentExists(string subdivisionCode);
    Task<bool> DepartmentExistsAsync(string subdivisionCode);

    ICollection<Employee>? GetEmployees(string subdivisionCode);
    Task<ICollection<Employee>>? GetEmployeesAsync(string subdivisionCode);

    #region CRUD

    bool CreateDepartment(Department department);
    bool UpdateDepartment(Department department);
    bool DeleteDepartment(string subdivisionCode);
    bool Save();

    Task<bool> CreateDepartmentAsync(Department department);
    Task<bool> UpdateDepartmentAsync(Department department);
    Task<bool> DeleteDepartmentAsync(string subdivisionCode);
    Task<bool> SaveAsync();

    #endregion CRUD
}
