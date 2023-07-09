using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;

namespace ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;

/// <summary>
/// Implements the Repository pattern, 
/// presents an interface for the Project class
/// </summary>
public interface IProjectCRUD
{
    #region CRUD
    bool CreateProject(ProjectDto project);
    bool UpdateProject(ProjectDto project);
    bool DeleteProject(string projectCode);
    bool Save();

    Task<bool> CreateProjectAsync(ProjectDto project);
    Task<bool> UpdateProjectAsync(ProjectDto project);
    Task<bool> DeleteProjectAsync(string projectCode);
    Task<bool> SaveAsync();
    #endregion CRUD
}
