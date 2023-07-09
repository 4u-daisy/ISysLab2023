using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;

namespace ISysLab2023.Backend.Lib.Core.IService.IPerson;

/// <summary>
/// Implements the Repository pattern, 
/// presents an interface for the Employee class
/// </summary>
public interface IEmployeeCRUD
{
    #region CRUD

    bool CreateEmployee(EmployeeDto employee);
    bool UpdateEmployee(EmployeeDto employee);
    bool DeleteEmployee(int employeeCode);
    bool Save();

    Task<bool> CreateEmployeeAsync(EmployeeDto employee);
    Task<bool> UpdateEmployeeAsync(EmployeeDto employee);
    Task<bool> DeleteEmployeeAsync(int employeeCode);
    Task<bool> SaveAsync();

    #endregion CRUD
}
