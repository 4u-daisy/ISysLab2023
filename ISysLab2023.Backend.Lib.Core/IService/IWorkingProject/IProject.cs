using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;

namespace ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;

/// <summary>
/// Implements the Repository pattern, 
/// presents an interface for the Project class
/// </summary>
public interface IProject
{
    // TO DO add pagination

    #region BasicQueries
    ICollection<Project>? GetProjects();
    Task<ICollection<Project>>? GetProjectsAsync();

    Project? GetProjectByCode(string projectCode);
    Task<Project>? GetProjectByCodeAsync(string projectCode);

    bool ProjectExists(string projectCode);
    Task<bool> ProjectExistsAsync(string projectCode);

    bool ParticipatesInProject(string projectCode, int codeEmployee);
    Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee);

    ICollection<Employee>? GetAllEmployeesInProject(string projectCode);
    Task<ICollection<Employee>> GetAllEmployeesInProjectAsync(string projectCode);
    #endregion BasicQueries

    #region CRUD
    bool CreateProject(Project project);
    bool UpdateProject(Project project);
    bool DeleteProject(string projectCode);
    bool Save();

    Task<bool> CreateProjectAsync(Project project);
    Task<bool> UpdateProjectAsync(Project project);
    Task<bool> DeleteProjectAsync(string projectCode);
    Task<bool> SaveAsync();
    #endregion CRUD
}
