namespace ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;

/// <summary>
/// Service interface for adding and removing 
/// employees from projects
/// </summary>
public interface IActionRemove
{
    #region RemoveEmployeeFromProject
    bool RemoveEmployeeFromProject(string projectCode, int employeeCode);

    Task<bool> RemoveEmployeeFromProjectAsync(string projectCode, int employeeCode);
    #endregion RemoveEmployeeFromProject

    #region RemoveEmployeeFromAllProject
    bool RemoveEmployeeFromAllProject(int employeeCode);
    Task<bool> RemoveEmployeeFromAllProjectAsync(int employeeCode);
    #endregion RemoveEmployeeFromAllProject

    #region RemoveProjectFromAllEmployees
    bool RemoveProjectFromAllEmployees(string projectCode);
    Task<bool> RemoveProjectFromAllEmployeesAsync(string projectCode);
    #endregion RemoveProjectFromAllEmployees
}
