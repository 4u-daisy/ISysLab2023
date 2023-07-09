using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;

namespace ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;
public interface IProjectBasicQueries
{
    #region BasicQueries
    ICollection<ProjectDto>? GetProjects(int page = 1);
    Task<ICollection<ProjectDto>>? GetProjectsAsync(int page = 1);

    ProjectDto? GetProjectByCode(string projectCode);
    Task<ProjectDto>? GetProjectByCodeAsync(string projectCode);

    bool ProjectExists(string projectCode);
    Task<bool> ProjectExistsAsync(string projectCode);

    bool ParticipatesInProject(string projectCode, int codeEmployee);
    Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee);

    ICollection<EmployeeDto>? GetAllEmployeesInProject(string projectCode, int page = 1);
    Task<ICollection<EmployeeDto>> GetAllEmployeesInProjectAsync(string projectCode, int page = 1);
    #endregion BasicQueries
}
