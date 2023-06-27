using FluentValidation;
using ISysLab2023.Backend.Lib.Core.IService.IPerson;
using ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;
using ISysLab2023.Backend.Lib.Core.Repository.SupportClassesRepository;
using ISysLab2023.Backend.Lib.Core.Validator.WorkingProjectValidator;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.Lib.Domain.Person;
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
    private readonly ProjectValidator _validator;
    public ProjectRepository(DataBaseContext dbContext, 
        ProjectValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    #region BasicQueries
    public ICollection<Employee>? GetAllEmployeesInProject
        (string projectCode) =>
        new EmployeeProjectsRepository(_dbContext)
        .GetAllEmployeesInProject(projectCode);

    public async Task<ICollection<Employee>> GetAllEmployeesInProjectAsync
        (string projectCode) =>
        await new EmployeeProjectsRepository(_dbContext)
        .GetAllEmployeesInProjectAsync(projectCode);

    public Project? GetProjectByCode(string projectCode) =>
        _dbContext.Projects.FirstOrDefault(x => x.ProjectCode == projectCode);

    public async Task<Project>? GetProjectByCodeAsync
        (string projectCode) =>
        await _dbContext.Projects
        .FirstOrDefaultAsync(x => x.ProjectCode == projectCode);

    public ICollection<Project>? GetProjects() =>
        _dbContext.Projects.ToList();

    public async Task<ICollection<Project>>? GetProjectsAsync() =>
        await _dbContext.Projects.ToListAsync();

    public bool ParticipatesInProject(string projectCode, int employeeCode) =>
        new EmployeeProjectsRepository(_dbContext)
        .ParticipatesInProject(projectCode, employeeCode);

    public async Task<bool> ParticipatesInProjectAsync
        (string projectCode, int employeeCode) =>
        await new EmployeeProjectsRepository(_dbContext)
        .ParticipatesInProjectAsync(projectCode, employeeCode);

    public bool ProjectExists(string projectCode) =>
        _dbContext.Projects
        .FirstOrDefault(x => x.ProjectCode == projectCode) == null ? 
        false : true;

    public async Task<bool> ProjectExistsAsync(string projectCode) =>
        await _dbContext.Projects
        .FirstOrDefaultAsync(x => x.ProjectCode == projectCode) == null ?
        false : true;

    #endregion BasicQueries

    #region CRUD

    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;

    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;

    public bool CreateProject(Project project)
    {
        _validator.ValidateAndThrow(project);
        _dbContext.Projects.Add(project);
        return Save();
    }

    public async Task<bool> CreateProjectAsync(Project project)
    {
        await _validator.ValidateAndThrowAsync(project);
        _dbContext.Projects.Add(project);
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

    public bool UpdateProject(Project project)
    {
        _validator.ValidateAndThrow(project);
        _dbContext.Projects.Update(project);
        return Save();
    }

    public async Task<bool> UpdateProjectAsync(Project project)
    {
        await _validator.ValidateAndThrowAsync(project);
        _dbContext.Projects.Update(project);
        return await SaveAsync();
    }
    #endregion CRUD
}
