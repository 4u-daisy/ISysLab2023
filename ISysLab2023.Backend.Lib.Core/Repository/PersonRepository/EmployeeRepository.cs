using ISysLab2023.Backend.Lib.Core.IService.IPerson;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;
using ISysLab2023.Backend.Lib.Core.MyMapping.PersonMapping;
using ISysLab2023.Backend.Lib.Core.MyMapping.WorkingProjectsMapping;
using ISysLab2023.Backend.Lib.Core.Repository.SupportClassesRepository;
using ISysLab2023.Backend.Lib.Core.Service;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
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
    public EmployeeRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
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

    public EmployeeDto? GetEmployeeByCode(int employeeCode) =>
        EmployeeMapping.Mapping(_dbContext.Employees
            .Include(x => x.Department)
        .FirstOrDefault(x => x.EmployeeCode == employeeCode));
    public async Task<EmployeeDto>? GetEmployeeByCodeAsync(int employeeCode) =>
        EmployeeMapping.Mapping(await _dbContext.Employees
            .Include(x => x.Department)
        .FirstOrDefaultAsync(x => x.EmployeeCode == employeeCode));

    public ICollection<EmployeeDto>? GetEmployees(int page = 1) =>
        EmployeeMapping.Mapping(PagedList<Employee>
            .Create(_dbContext.Employees.Include(x => x.Department)
            .ToList(), page))
        !.ToList();
    public async Task<ICollection<EmployeeDto>>? GetEmployeesAsync(
        int page = 1) =>
        EmployeeMapping.Mapping(PagedList<Employee>
            .Create(await _dbContext.Employees.Include(x => x.Department)
            .ToListAsync(), page))
        !.ToList();

    public ICollection<ProjectDto>? GetProject(
        int employeeCode, int page = 1) =>
        ProjectMapping.Mapping(PagedList<Project>
            .Create(_dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project).ToList(), page))
        !.ToList();
    public async Task<ICollection<ProjectDto>>? GetProjectsAsync(
        int employeeCode, int page = 1) =>
        ProjectMapping.Mapping(PagedList<Project>
            .Create(await _dbContext.EmployeeProjects
            .Where(x => x.Employee.EmployeeCode == employeeCode)
            .Select(x => x.Project).ToListAsync(), page))
        !.ToList();

    public ICollection<EmployeeDto>? GetSubEmployees(
        int employeeCode, int page = 1) =>
        EmployeeMapping.Mapping(PagedList<Employee>
            .Create(_dbContext.Employees.Include(x => x.Department)
            .Include(x => x.Department)
            .Where(x => x.HeadManager!.EmployeeCode == employeeCode)
            .ToList(), page))
        !.ToList();

    public async Task<ICollection<EmployeeDto>>? GetSubEmployeesAsync(
        int employeeCode, int page = 1) =>
        EmployeeMapping.Mapping(PagedList<Employee>
            .Create(await _dbContext.Employees.Include(x => x.Department)
            .Where(x => x.HeadManager!.EmployeeCode == employeeCode)
            .ToListAsync(), page))
        !.ToList();

    public bool ParticipatesInProject(string projectCode, int employeeCode) =>
        new EmployeeProjectRepository(_dbContext)
        .ParticipatesInProject(projectCode, employeeCode);
    public async Task<bool> ParticipatesInProjectAsync(string projectCode,
        int employeeCode) =>
        await new EmployeeProjectRepository(_dbContext)
        .ParticipatesInProjectAsync(projectCode, employeeCode);

    #endregion BasicQueries

    #region CRUD
    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;

    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;

    public bool CreateEmployee(EmployeeDto employeeDto)
    {
        if (employeeDto == null)
            return false;

        var employee = EmployeeMapping.Mapping(employeeDto);
        employee!.Department = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode);
        employee!.IdDepartment = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode).Id;

        _dbContext.Employees.Add(employee);
        return Save();
    }

    public async Task<bool> CreateEmployeeAsync(EmployeeDto employeeDto)
    {
        if (employeeDto == null)
            return false;

        var employee = EmployeeMapping.Mapping(employeeDto);
        employee!.Department = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode);
        employee!.IdDepartment = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode).Id;
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

    public bool UpdateEmployee(EmployeeDto employeeDto)
    {
        var employee = EmployeeMapping.Mapping(employeeDto);
        employee!.Department = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode);
        employee!.IdDepartment = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode).Id;

        _dbContext.Employees.Update(employee);
        return Save();
    }

    public async Task<bool> UpdateEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = EmployeeMapping.Mapping(employeeDto);
        employee!.Department = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode);
        employee!.IdDepartment = _dbContext.Departments
            .First(x => x.SubdivisionCode == employeeDto.DepartmentCode).Id;

        _dbContext.Employees.Update(employee);
        return await SaveAsync();
    }
    #endregion CRUD
}
