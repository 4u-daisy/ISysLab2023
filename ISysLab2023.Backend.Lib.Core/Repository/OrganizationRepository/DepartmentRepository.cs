using FluentValidation;
using ISysLab2023.Backend.Lib.Core.IService.IOrganization;
using ISysLab2023.Backend.Lib.Core.Validator.OrganizationValidator;
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
    private readonly DepartmentValidator _validator;
    public DepartmentRepository(DataBaseContext dbContext,
        DepartmentValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    #region BasicQueries
    public bool DepartmentExists(string subdivisionCode) =>
        _dbContext.Departments
        .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode) == null ?
        false : true;

    public async Task<bool> DepartmentExistsAsync(string subdivisionCode) =>
        await _dbContext.Departments
        .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode) == null ?
        false : true;

    public Department? GetDepartmentByCode(string subdivisionCode) =>
        _dbContext.Departments
        .FirstOrDefault(x => x.SubdivisionCode == subdivisionCode);

    public async Task<Department>? GetDepartmentByCodeAsync(string subdivisionCode) =>
        await _dbContext.Departments
        .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode);

    public ICollection<Department>? GetDepartments() =>
        _dbContext.Departments.ToList();

    public async Task<ICollection<Department>>? GetDepartmentsAsync() =>
        await _dbContext.Departments.ToListAsync();

    public ICollection<Employee>? GetEmployees(string subdivisionCode) =>
        _dbContext.Employees
        .Where(x => x.Department.SubdivisionCode == subdivisionCode)
        .ToList();

    public async Task<ICollection<Employee>>? GetEmployeesAsync(string subdivisionCode) =>
        await _dbContext.Employees
        .Where(x => x.Department.SubdivisionCode == subdivisionCode)
        .ToListAsync();
    #endregion BasicQueries

    #region CRUD

    public bool Save() =>
        _dbContext.SaveChanges() > 0 ? true : false;

    public async Task<bool> SaveAsync() =>
        await _dbContext.SaveChangesAsync() > 0 ? true : false;

    public bool CreateDepartment(Department department)
    {
        _validator.ValidateAndThrow(department);
        _dbContext.Departments.Add(department);
        return Save();
    }

    public async Task<bool> CreateDepartmentAsync(Department department)
    {
        await _validator.ValidateAndThrowAsync(department);
        await _dbContext.Departments.AddAsync(department);
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

    public async Task<bool> DeleteDepartmentAsync(string subdivisionCode)
    {
        var department = await _dbContext.Departments
            .FirstOrDefaultAsync(x => x.SubdivisionCode == subdivisionCode);
        if (department == null)
            return false;
        _dbContext.Departments.Remove(department);
        return await SaveAsync();
    }

    // TODO Dto Model provides a new model without Id.
    // And the system cannot update a model with a different Id.
    // I don't know how to do it :(
    public bool UpdateDepartment(Department department)
    {
        _validator.ValidateAndThrow(department);
        _dbContext.Departments.Update(department);
        return Save();
    }

    public async Task<bool> UpdateDepartmentAsync(Department department)
    {
        await _validator.ValidateAndThrowAsync(department);
        _dbContext.Departments.Update(department);
        return await SaveAsync();
    }
    #endregion CRUD
}
