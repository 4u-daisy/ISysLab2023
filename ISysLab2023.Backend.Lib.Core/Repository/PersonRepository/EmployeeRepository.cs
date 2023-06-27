using FluentValidation;
using ISysLab2023.Backend.Lib.Core.IService.IPerson;
using ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
using ISysLab2023.Backend.Lib.Core.Repository.SupportClassesRepository;
using ISysLab2023.Backend.Lib.Core.Validator.PersonValidator;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using Microsoft.EntityFrameworkCore;

namespace ISysLab2023.Backend.Lib.Core.Repository.PersonRepository;
/// <summary>
/// Provides an implementation of the interface for 
/// interaction with the employee
/// </summary>
public class EmployeeRepository : IEmployee
{
    private readonly DataBaseContext _dbContext;
    private readonly EmployeeValidator _validator;
    public EmployeeRepository(DataBaseContext dbContext, 
        EmployeeValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    #region BasicQueries
    public bool EmployeeExists(int employeeCode) =>
        _dbContext.Employees
        .FirstOrDefault(x => x.EmployeeCode == employeeCode) == null ? 
        false : true;
    public async Task<bool> EmployeeExistsAsync(int employeeCode) =>
        await _dbContext.Employees
        .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode) == null ?
        false : true;

    public Employee? GetEmployeeByCode(int employeeCode) =>
        _dbContext.Employees
        .FirstOrDefault(x => x.EmployeeCode == employeeCode);
    public async Task<Employee>? GetEmployeeByCodeAsync(int employeeCode) =>
        await _dbContext.Employees
        .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);

    public ICollection<Employee>? GetEmployees() =>
        _dbContext.Employees.ToList();
    public async Task<ICollection<Employee>>? GetEmployeesAsync() =>
        await _dbContext.Employees.ToListAsync();

    public ICollection<Project>? GetProject(int employeeCode) =>
        _dbContext.EmployeeProjects
        .Where(x => x.Employee.EmployeeCode == employeeCode)
        .Select(x => x.Project)
        .ToList();
    public async Task<ICollection<Project>>? GetProjectsAsync(int employeeCode) =>
        await _dbContext.EmployeeProjects
        .Where(x => x.Employee.EmployeeCode == employeeCode)
        .Select(x => x.Project)
        .ToListAsync();

    public ICollection<Employee>? GetSubEmployees(int employeeCode) =>
        _dbContext.Employees
        .Where(x => x.HeadManager!.EmployeeCode == employeeCode)
        .ToList();
    public async Task<ICollection<Employee>>? GetSubEmployeesAsync(int employeeCode) =>
        await _dbContext.Employees
        .Where(x => x.HeadManager!.EmployeeCode == employeeCode)
        .ToListAsync();

    public bool ParticipatesInProject(string projectCode, int employeeCode) =>
        new EmployeeProjectsRepository(_dbContext)
        .ParticipatesInProject(projectCode, employeeCode);

    public async Task<bool> ParticipatesInProjectAsync(string projectCode,
        int employeeCode) =>
        await new EmployeeProjectsRepository(_dbContext)
        .ParticipatesInProjectAsync(projectCode, employeeCode);
    #endregion BasicQueries

    #region CRUD

    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;

    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;

    public bool CreateEmployee(Employee employee)
    {
        _validator.ValidateAndThrow(employee);
        _dbContext.Employees.Add(employee);
        return Save();
    }

    public async Task<bool> CreateEmployeeAsync(Employee employee)
    {
        await _validator.ValidateAndThrowAsync(employee);
        await _dbContext.Employees.AddAsync(employee);
        return await SaveAsync();
    }

    public bool DeleteEmployee(int employeeCode)
    {
        var employee = _dbContext.Employees
            .FirstOrDefault(x => x.EmployeeCode == employeeCode);
        if (employee == null)
            return false;
        _dbContext.Employees.Remove(employee);

        return Save();
    }

    public async Task<bool> DeleteEmployeeAsync(int employeeCode)
    {
        var employee = await _dbContext.Employees
            .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode);
        if (employee == null)
            return false;
        _dbContext.Employees.Remove(employee);
        return await SaveAsync();
    }

    public bool UpdateEmployee(Employee employee)
    {
        _validator.ValidateAndThrow(employee);

        _dbContext.Employees.Update(employee);
        return Save();
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        await _validator.ValidateAndThrowAsync(employee);

        _dbContext.Employees.Update(employee);
        return await SaveAsync();
    }
    #endregion CRUD
}
