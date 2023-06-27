using ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
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
public class EmployeeProjectsRepository : IEmployeeProjects
{
    private readonly DataBaseContext _dbContext;
    public EmployeeProjectsRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

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
            IdProject = project.Id
        });

        return Save();
    }

    public bool AddEmployeeInProject(Project project, Employee employee)
    {
        if (_dbContext.Projects.Find(project) == null)
        {
            // TODO add logger
            return false;
        }
        if (_dbContext.Employees.Find(employee) == null)
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

    public async Task<bool> AddEmployeeInProjectAsync(Project project, 
        Employee employee)
    {
        if (await _dbContext.Projects.FindAsync(project) == null)
        {
            // TODO add logger
            return false;
        }
        if (await _dbContext.Employees.FindAsync(employee) == null)
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
    public ICollection<Employee>? GetAllEmployeesInProject(string projectCode) =>
        _dbContext.EmployeeProjects
        .Where(x => x.Project.ProjectCode == projectCode)
        .Select(x => x.Employee)
        .ToList();

    public async Task<ICollection<Employee>> GetAllEmployeesInProjectAsync
        (string projectCode) =>
        await _dbContext.EmployeeProjects
        .Where(x => x.Project.ProjectCode == projectCode)
        .Select(x => x.Employee)
        .ToListAsync();
    #endregion GetAllEmployeesInProject

    #region GetAllProjectsEmployee
    public ICollection<Project>? GetAllProjectsEmployee(int employeeCode) =>
        _dbContext.EmployeeProjects
        .Where(x => x.Employee.EmployeeCode == employeeCode)
        .Select(x => x.Project)
        .ToList();

    public async Task<ICollection<Project>>? GetAllProjectsEmployeeAsync
        (int employeeCode) =>
        await _dbContext.EmployeeProjects
        .Where(x => x.Employee.EmployeeCode == employeeCode)
        .Select(x => x.Project)
        .ToListAsync();
    #endregion GetAllProjectsEmployee

    #region CheckExists
    public bool ParticipatesInProject(string projectCode, int codeEmployee) =>
        _dbContext.EmployeeProjects
        .FirstOrDefault(x => x.Employee!.EmployeeCode == codeEmployee &&
        x.Project!.ProjectCode == projectCode) == null ? false : true;

    public async Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee) =>
        await _dbContext.EmployeeProjects
        .FirstOrDefaultAsync(x => x.Employee!.EmployeeCode == codeEmployee &&
        x.Project!.ProjectCode == projectCode) == null ? false : true;
    #endregion CheckExists

    #region Remove
    public bool RemoveEmployeeFromProject(string projectCode, int employeeCode)
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

    public bool RemoveEmployeeFromProject(Project project, Employee employee)
    {
        var employeeProject = _dbContext.EmployeeProjects
            .FirstOrDefault(x => x.Project!.Equals(project) &&
            x.Employee!.Equals(employee));

        if (employeeProject == null)
        {
            // TODO add logger
            return false;
        }
        _dbContext.EmployeeProjects.Remove(employeeProject);
        return Save();
    }

    public async Task<bool> RemoveEmployeeFromProjectAsync(string projectCode, int employeeCode)
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

    public async Task<bool> RemoveEmployeeFromProjectAsync(Project project, Employee employee)
    {
        var employeeProject = await _dbContext.EmployeeProjects
          .FirstOrDefaultAsync(x => x.Project!.Equals(project) &&
          x.Employee!.Equals(employee));

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

    // TODO I'm not sure if there is a cascade deletion. Check it :/

    #region RemoveEmployeeFromAllProject
    public bool RemoveEmployeeFromAllProject(int employeeCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
                .Where(x => x.Employee!.EmployeeCode == employeeCode)
                .ToList());

        return Save();
    }
    public async Task<bool> RemoveEmployeeFromAllProjectAsync(int employeeCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
                .Where(x => x.Employee!.EmployeeCode == employeeCode)
                .ToList());

        return await SaveAsync();
    }

    #endregion RemoveEmployeeFromAllProject

    #region RemoveProjectFromAllEmployees
    public bool RemoveProjectFromAllEmployees(string projectCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
            .Where(x => x.Project!.ProjectCode == projectCode)
            .ToList());

        return Save();
    }
    public async Task<bool> RemoveProjectFromAllEmployeesAsync(string projectCode)
    {
        _dbContext.EmployeeProjects
            .RemoveRange(_dbContext.EmployeeProjects
            .Where(x => x.Project!.ProjectCode == projectCode)
            .ToList());
        return await SaveAsync();
    }
    #endregion RemoveProjectFromAllEmployees
}
