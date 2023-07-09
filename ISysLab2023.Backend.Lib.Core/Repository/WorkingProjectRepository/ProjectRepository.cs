using ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;
using ISysLab2023.Backend.Lib.Core.MyMapping.WorkingProjectsMapping;
using ISysLab2023.Backend.Lib.Core.Repository.SupportClassesRepository;
using ISysLab2023.Backend.Lib.Core.Service;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using Microsoft.EntityFrameworkCore;

namespace ISysLab2023.Backend.Lib.Core.Repository.WorkingProjectRepository;
/// <summary>
/// Provides an implementation of the interface for 
/// interaction with the project
/// </summary>
public class ProjectRepository : IProject
{
    private readonly DataBaseContext _dbContext;
    public ProjectRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region BasicQueries
    public ICollection<EmployeeDto>? GetAllEmployeesInProject(
        string projectCode, int page = 1) =>
        new EmployeeProjectRepository(_dbContext)
        .GetAllEmployeesInProject(projectCode, page);

    public async Task<ICollection<EmployeeDto>> GetAllEmployeesInProjectAsync(
        string projectCode, int page = 1) =>
        await new EmployeeProjectRepository(_dbContext)
        .GetAllEmployeesInProjectAsync(projectCode, page);

    public ProjectDto? GetProjectByCode(string projectCode) =>
        ProjectMapping.Mapping(_dbContext.Projects
            .FirstOrDefault(x => x.ProjectCode == projectCode));

    public async Task<ProjectDto>? GetProjectByCodeAsync(
        string projectCode) =>
        ProjectMapping.Mapping(await _dbContext.Projects
        .FirstOrDefaultAsync(x => x.ProjectCode == projectCode));

    public ICollection<ProjectDto>? GetProjects(int page = 1) =>
        ProjectMapping.Mapping(PagedList<Project>
            .Create(_dbContext.Projects.ToList(), page))
        !.ToList();

    public async Task<ICollection<ProjectDto>>? GetProjectsAsync(
        int page = 1) =>
        ProjectMapping.Mapping(PagedList<Project>
            .Create(await _dbContext.Projects.ToListAsync(), page))
        !.ToList();

    public bool ParticipatesInProject(
        string projectCode, int employeeCode) =>
        new EmployeeProjectRepository(_dbContext)
        .ParticipatesInProject(projectCode, employeeCode);

    public async Task<bool> ParticipatesInProjectAsync(
        string projectCode, int employeeCode) =>
        await new EmployeeProjectRepository(_dbContext)
        .ParticipatesInProjectAsync(projectCode, employeeCode);

    public bool ProjectExists(string projectCode) =>
        _dbContext.Projects
        .FirstOrDefault(x => x.ProjectCode == projectCode) ==
        null ? false : true;

    public async Task<bool> ProjectExistsAsync(string projectCode) =>
        await _dbContext.Projects
        .FirstOrDefaultAsync(x => x.ProjectCode == projectCode) ==
        null ? false : true;

    #endregion BasicQueries

    #region CRUD

    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;

    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;

    public bool CreateProject(ProjectDto project)
    {
        _dbContext.Projects.Add(ProjectMapping.Mapping(project));
        return Save();
    }

    public async Task<bool> CreateProjectAsync(ProjectDto project)
    {
        _dbContext.Projects.Add(ProjectMapping.Mapping(project));
        return await SaveAsync();
    }

    public bool DeleteProject(string projectCode)
    {
        var project = _dbContext.Projects
            .FirstOrDefault(x => x.ProjectCode == projectCode);
        if (project == null)
            return false;

        _dbContext.Projects.Remove(project);
        return Save();
    }

    public async Task<bool> DeleteProjectAsync(string projectCode)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(x => x.ProjectCode == projectCode);
        if (project == null)
            return false;

        _dbContext.Projects.Remove(project);
        return await SaveAsync();
    }

    public bool UpdateProject(ProjectDto project)
    {
        _dbContext.Projects.Update(ProjectMapping.Mapping(project));
        return Save();
    }

    public async Task<bool> UpdateProjectAsync(ProjectDto project)
    {
        _dbContext.Projects.Update(ProjectMapping.Mapping(project));
        return await SaveAsync();
    }
    #endregion CRUD
}
