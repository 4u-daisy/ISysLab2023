using ISysLab2023.Backend.Lib.Core.IService.IPerson;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using Microsoft.EntityFrameworkCore;

namespace ISysLab2023.Backend.Lib.Core.Repository.PersonRepository;
public class EmployeeRepository : IEmployee
{
    private readonly DataBaseContext _dbContext;
    public EmployeeRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool CreateEmployee(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        return Save();
    }

    public async Task<bool> CreateEmployeeAsync(Employee employee)
    {
        await _dbContext.Employees.AddAsync(employee);
        return await SaveAsync();
    }

    public bool DeleteEmployee(int codeEmployee)
    {
        var employee = _dbContext.Employees
            .FirstOrDefault(x => x.CodeEmployee == codeEmployee);
        if (employee == null)
            return false;
        _dbContext.Employees.Remove(employee);
        return Save();
    }

    public async Task<bool> DeleteEmployeeAsync(int codeEmployee)
    {
        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(x => x.CodeEmployee == codeEmployee);
        if (employee == null)
            return false;
        _dbContext.Employees.Remove(employee);
        return await SaveAsync();
    }

    public bool EmployeeExists(int codeEmployee) =>
        _dbContext.Employees
        .FirstOrDefault(x => x.CodeEmployee == codeEmployee) == null ? 
        false : true;

    public async Task<bool> EmployeeExistsAsync(int codeEmployee) =>
        await _dbContext.Employees
        .FirstOrDefaultAsync(x => x.CodeEmployee == codeEmployee) == null ?
        false : true;

    public Employee? GetEmployeeByCode(int codeEmployee) =>
        _dbContext.Employees
        .FirstOrDefault(x => x.CodeEmployee == codeEmployee);

    public async Task<Employee>? GetEmployeeByCodeAsync(int codeEmployee) =>
        await _dbContext.Employees
        .FirstOrDefaultAsync(x => x.CodeEmployee == codeEmployee);

    public ICollection<Employee>? GetEmployees() =>
        _dbContext.Employees.ToList();

    public async Task<ICollection<Employee>>? GetEmployeesAsync() =>
        await _dbContext.Employees.ToListAsync();

    public ICollection<Project>? GetProject(int codeEmployee) =>
        _dbContext.EmployeeProjects
        .Where(x => x.Employee.CodeEmployee == codeEmployee)
        .Select(x => x.Project)
        .ToList();

    public async Task<ICollection<Project>>? GetProjectsAsync(int codeEmployee) =>
        await _dbContext.EmployeeProjects
        .Where(x => x.Employee.CodeEmployee == codeEmployee)
        .Select(x => x.Project)
        .ToListAsync();

    public ICollection<Employee>? GetSubEmployees(int codeEmployee) =>
        _dbContext.Employees
        .Where(x => x.HeadManager!.CodeEmployee == codeEmployee)
        .ToList();

    public async Task<ICollection<Employee>>? GetSubEmployeesAsync(int codeEmployee) =>
        await _dbContext.Employees
        .Where(x => x.HeadManager!.CodeEmployee == codeEmployee)
        .ToListAsync();

    public bool ParticipatesInProject(string projectCode, int codeEmployee) =>
        _dbContext.EmployeeProjects
        .FirstOrDefault(x => x.Employee!.CodeEmployee == codeEmployee && 
        x.Project!.ProjectCode == projectCode) == null ? false : true;

    public async Task<bool> ParticipatesInProjectAsync(string projectCode, 
        int codeEmployee) =>
        await _dbContext.EmployeeProjects
        .FirstOrDefaultAsync(x => x.Employee!.CodeEmployee == codeEmployee &&
        x.Project!.ProjectCode == projectCode) == null ? false : true;


    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;

    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;

    public bool UpdateEmployee(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        return Save();
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        return await SaveAsync();
    }
}
