using ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.SupportClassDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;
using ISysLab2023.Backend.Lib.Core.MyMapping.PersonMapping;
using ISysLab2023.Backend.Lib.Core.MyMapping.WorkingProjectsMapping;
using ISysLab2023.Backend.Lib.Core.Service;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.SupportClasses;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using Microsoft.EntityFrameworkCore;

namespace ISysLab2023.Backend.Lib.Core.Repository.SupportClassesRepository;
/// <summary>
/// Provides an interface for adding and removing 
/// employees from a project
/// </summary>
public class EmployeeProjectRepository : IEmployeeProject
{
    private readonly DataBaseContext _dbContext;
    public EmployeeProjectRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Show

    public ICollection<EmployeeProjectDto>? GetAllEmployeeProject(
        int page = 1) =>
        PagedList<EmployeeProjects>
        .Create(_dbContext.EmployeeProjects
            .Include(x => x.Employee)
            .Include(x => x.Project)
            .ToList(), page)
        .ToList()
        .Select(x => new EmployeeProjectDto()
        {
            EmployeeCode = x.Employee!.EmployeeCode,
            ProjectCode = x.Project!.ProjectCode
        }).ToList();

    public async Task<ICollection<EmployeeProjectDto>>? GetAllEmployeeProjectAsync(
        int page = 1) =>
        PagedList<EmployeeProjects>
        .Create(await _dbContext.EmployeeProjects
            .Include(x => x.Employee)
            .Include(x => x.Project)
            .ToListAsync(), page)
        .ToList()
        .Select(x => new EmployeeProjectDto()
        {
            EmployeeCode = x.Employee!.EmployeeCode,
            ProjectCode = x.Project!.ProjectCode
        }).ToList();

    #endregion Show

    #region Add

    public bool AddEmployeeInProject(string projectCode, int employeeCode)
    {
        var project = _dbContext.Projects
            .FirstOrDefault(x => x.ProjectCode == projectCode);
        if (project == null)
        {
            // TODO add logger
            return false;
        }

        var employee = _dbContext.Employees
            .FirstOrDefault(x => x.EmployeeCode == employeeCode);
        if (employee == null)
        {
            // TODO add logger
            return false;
        }

        _dbContext.EmployeeProjects.Add(new EmployeeProjects
        {
            Employee = employee,
            IdEmployee = employee.Id,
            Project = project,
            IdProject = project.Id,
            Key = project.Id + "_" + employee.Id
        });

        return Save();
    }

    public async Task<bool> AddEmployeeInProjectAsync(string projectCode,
        int employeeCode)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(x => x.ProjectCode == projectCode);
        if (project == null)
        {
            // TODO add logger
            return false;
        }

        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);
        if (employee == null)
        {
            // TODO add logger
            return false;
        }

        _dbContext.EmployeeProjects.Add(new EmployeeProjects
        {
            Employee = employee,
            IdEmployee = employee.Id,
            Project = project,
            IdProject = project.Id
        });

        return await SaveAsync();
    }


    #endregion Add

    #region GetAllEmployeesInProject
    public ICollection<EmployeeDto>? GetAllEmployeesInProject(
        string projectCode, int page = 10) =>
        EmployeeMapping.Mapping(PagedList<Employee>
            .Create(_dbContext.EmployeeProjects
            .Where(x => x.Project.ProjectCode == projectCode)
            .Select(x => x.Employee)
            .ToList(), page))
        !.ToList();

    public async Task<ICollection<EmployeeDto>> GetAllEmployeesInProjectAsync(
        string projectCode, int page) =>
        EmployeeMapping.Mapping(PagedList<Employee>
            .Create(await _dbContext.EmployeeProjects
            .Where(x => x.Project.ProjectCode == projectCode)
            .Select(x => x.Employee)
            .ToListAsync(), page))
        !.ToList();


    #endregion GetAllEmployeesInProject

    #region GetAllProjectsEmployee
    public ICollection<ProjectDto>? GetAllProjectsEmployee(
        int employeeCode, int page = 10) =>
        ProjectMapping.Mapping(PagedList<Project>
            .Create(_dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project)
            .ToList(), page))
        !.ToList();

    public async Task<ICollection<ProjectDto>>? GetAllProjectsEmployeeAsync(
        int employeeCode, int page = 10) =>
        ProjectMapping.Mapping(PagedList<Project>
            .Create(await _dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project)
            .ToListAsync(), page))
        .ToList();

    #endregion GetAllProjectsEmployee

    #region CheckExists
    public bool ParticipatesInProject(string projectCode, int codeEmployee) =>
        _dbContext.EmployeeProjects
        .FirstOrDefault(x => x.Employee!.EmployeeCode == codeEmployee &&
        x.Project!.ProjectCode == projectCode) == null ? false : true;

    public async Task<bool> ParticipatesInProjectAsync(
        string projectCode, int codeEmployee) =>
        await _dbContext.EmployeeProjects
        .FirstOrDefaultAsync(x => x.Employee!.EmployeeCode == codeEmployee &&
        x.Project!.ProjectCode == projectCode) == null ? false : true;
    #endregion CheckExists

    #region Remove
    public bool RemoveEmployeeFromProject(
        string projectCode, int employeeCode)
    {
        var employeeProject = _dbContext.EmployeeProjects
            .FirstOrDefault(x => x.Project!.ProjectCode == projectCode &&
            x.Employee!.EmployeeCode == employeeCode);

        if (employeeProject == null)
        {
            // TODO add logger
            return false;
        }
        _dbContext.EmployeeProjects.Remove(employeeProject);
        return Save();
    }
    public async Task<bool> RemoveEmployeeFromProjectAsync(
        string projectCode, int employeeCode)
    {
        var employeeProject = await _dbContext.EmployeeProjects
            .FirstOrDefaultAsync(x => x.Project!.ProjectCode == projectCode &&
            x.Employee!.EmployeeCode == employeeCode);

        if (employeeProject == null)
        {
            // TODO add logger
            return false;
        }
        _dbContext.EmployeeProjects.Remove(employeeProject);
        return await SaveAsync();
    }

    #endregion Remove

    #region Save
    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;
    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;
    #endregion Save

    #region RemoveEmployeeFromAllProject
    public bool RemoveEmployeeFromAllProject(
        int employeeCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
                .Where(x => x.Employee!.EmployeeCode == employeeCode)
                .ToList());

        return Save();
    }
    public async Task<bool> RemoveEmployeeFromAllProjectAsync(
        int employeeCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
                .Where(x => x.Employee!.EmployeeCode == employeeCode)
                .ToList());

        return await SaveAsync();
    }

    #endregion RemoveEmployeeFromAllProject

    #region RemoveProjectFromAllEmployees
    public bool RemoveProjectFromAllEmployees(
        string projectCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
            .Where(x => x.Project!.ProjectCode == projectCode)
            .ToList());

        return Save();
    }
    public async Task<bool> RemoveProjectFromAllEmployeesAsync(
        string projectCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
            .Where(x => x.Project!.ProjectCode == projectCode)
            .ToList());
        return await SaveAsync();
    }
    #endregion RemoveProjectFromAllEmployees
}
