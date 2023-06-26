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

    ICollection<Project>? GetProjects();
    Task<ICollection<Project>>? GetProjectsAsync();

    Project? GetProjectByCode(string code);
    Task<Project>? GetProjectByCodeAsync(string code);

    bool ProjectExists(string code);
    bool ProjectExistsAsync(string code);

    bool ParticipatesInProject(string projectCode, int codeEmployee);
    Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee);

    ICollection<Employee>? GetAllEmployeesInProject(string projectCode);
    Task<ICollection<Employee>> GetAllEmployeesInProjectAsync(string projectCode);
}
