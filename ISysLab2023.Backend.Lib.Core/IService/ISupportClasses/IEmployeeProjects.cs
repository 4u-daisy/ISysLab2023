using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;

namespace ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;

/// <summary>
/// Service interface for adding and removing 
/// employees from projects
/// </summary>
public interface IEmployeeProjects
{
    #region CheckExists
    bool ParticipatesInProject(string projectCode, int codeEmployee);
    Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee);
    #endregion CheckExists

    #region GetAllEmployeesInProject
    ICollection<Employee>? GetAllEmployeesInProject(string projectCode);
    Task<ICollection<Employee>> GetAllEmployeesInProjectAsync(string projectCode);
    #endregion GetAllEmployeesInProject

    #region GetAllProjectsEmployee
    ICollection<Project>? GetAllProjectsEmployee(int employeeCode);
    Task<ICollection<Project>>? GetAllProjectsEmployeeAsync(int employeeCode);
    #endregion GetAllProjectsEmployee

    #region AddEmployeeInProject
    bool AddEmployeeInProject(string projectCode, int employeeCode);
    bool AddEmployeeInProject(Project project, Employee employee);

    Task<bool> AddEmployeeInProjectAsync(string projectCode, int employeeCode);
    Task<bool> AddEmployeeInProjectAsync(Project project, Employee employee);
    #endregion AddEmployeeInProject

    #region RemoveEmployeeFromProject
    bool RemoveEmployeeFromProject(string projectCode, int employeeCode);
    bool RemoveEmployeeFromProject(Project project, Employee employee);

    Task<bool> RemoveEmployeeFromProjectAsync(string projectCode, int employeeCode);
    Task<bool> RemoveEmployeeFromProjectAsync(Project project, Employee employee);
    #endregion RemoveEmployeeFromProject

    #region Save
    bool Save();
    Task<bool> SaveAsync();
    #endregion Save

    // TODO I'm not sure if there is a cascade deletion. Check it :/

    #region RemoveEmployeeFromAllProject
    bool RemoveEmployeeFromAllProject(int employeeCode);
    Task<bool> RemoveEmployeeFromAllProjectAsync(int employeeCode);
    #endregion RemoveEmployeeFromAllProject

    #region RemoveProjectFromAllEmployees
    bool RemoveProjectFromAllEmployees(string projectCode);
    Task<bool> RemoveProjectFromAllEmployeesAsync(string projectCode);
    #endregion RemoveProjectFromAllEmployees
}
