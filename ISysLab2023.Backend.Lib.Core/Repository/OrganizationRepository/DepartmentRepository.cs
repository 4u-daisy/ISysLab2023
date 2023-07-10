using ISysLab2023.Backend.Lib.Core.IService.IOrganization;
using ISysLab2023.Backend.Lib.Core.ModelDto.OrganizationDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.MyMapping.OrganizationMapping;
using ISysLab2023.Backend.Lib.Core.MyMapping.PersonMapping;
using ISysLab2023.Backend.Lib.Core.Service;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.Lib.Domain.Person;
using Microsoft.EntityFrameworkCore;

namespace ISysLab2023.Backend.Lib.Core.Repository.OrganizationRepository;
/// <summary>
/// Provides an interface implementation 
/// for working with the Department 
/// </summary>
public class DepartmentRepository : IDepartment
{
    private readonly DataBaseContext _dbContext;
    public DepartmentRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region BasicQueries
    public bool DepartmentExists(string subdivisionCode) =>
        _dbContext.Departments
        .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode) ==
        null ? false : true;

    public async Task<bool> DepartmentExistsAsync(
        string subdivisionCode) =>
        await _dbContext.Departments
        .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode) ==
        null ? false : true;

    public DepartmentDto? GetDepartmentByCode(
        string subdivisionCode) =>
        DepartmentMapping.Mapping(_dbContext.Departments
        .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode));

    public async Task<DepartmentDto>? GetDepartmentByCodeAsync(
        string subdivisionCode) =>
        DepartmentMapping.Mapping(await _dbContext.Departments
        .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode));

    public ICollection<DepartmentDto>? GetDepartments(
        int page = 1) =>
        DepartmentMapping.Mapping(PagedList<Department>
            .Create(_dbContext.Departments.ToList(), page))
            !.ToList();

    public async Task<ICollection<DepartmentDto>>? GetDepartmentsAsync(
        int page = 1) =>
    DepartmentMapping.Mapping(PagedList<Department>
        .Create(await _dbContext.Departments.ToListAsync(), page))
        !.ToList();

    public ICollection<EmployeeDto>? GetEmployees(
        string subdivisionCode, int page = 1) =>
        EmployeeMapping.Mapping(PagedList<Employee>
        .Create(_dbContext.Employees.Include(x => x.Department)
            .Where(x => x.Department.SubdivisionCode == subdivisionCode)
            .ToList(), page))
        !.ToList();

    public async Task<ICollection<EmployeeDto>>? GetEmployeesAsync(
        string subdivisionCode, int page = 1) =>
        EmployeeMapping.Mapping(PagedList<Employee>
            .Create(await _dbContext.Employees.Include(x => x.Department)
            .Where(x => x.Department.SubdivisionCode == subdivisionCode)
            .ToListAsync(), page))
        !.ToList();


    #endregion BasicQueries

    #region CRUD
    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;

    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;

    public bool CreateDepartment(DepartmentDto department)
    {
        _dbContext.Departments
            .Add(DepartmentMapping.Mapping(department));
        return Save();
    }

    public async Task<bool> CreateDepartmentAsync(
        DepartmentDto department)
    {
        await _dbContext.Departments
            .AddAsync(DepartmentMapping.Mapping(department));
        return await SaveAsync();
    }

    public bool DeleteDepartment(string subdivisionCode)
    {
        var department = _dbContext.Departments
            .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode);
        if (department == null)
            return false;
        _dbContext.Departments.Remove(department);
        return Save();
    }

    public async Task<bool> DeleteDepartmentAsync(
        string subdivisionCode)
    {
        var department = await _dbContext.Departments
            .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode);
        if (department == null)
            return false;
        _dbContext.Departments.Remove(department);
        return await SaveAsync();
    }

    public bool UpdateDepartment(DepartmentDto department)
    {
        _dbContext.Departments
            .Update(DepartmentMapping.Mapping(department));
        return Save();
    }

    public async Task<bool> UpdateDepartmentAsync(
        DepartmentDto department)
    {
        _dbContext.Departments
            .Update(DepartmentMapping.Mapping(department));
        return await SaveAsync();
    }
    #endregion CRUD
}
