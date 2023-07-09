using ISysLab2023.Backend.Lib.Core.ModelDto.OrganizationDto;

namespace ISysLab2023.Backend.Lib.Core.IService.IOrganization;

/// <summary>
/// Implements the Repository pattern, 
/// presents an interface for the Department class
/// </summary>
public interface IDepartmentCRUD
{
    #region CRUD

    bool CreateDepartment(DepartmentDto department);
    bool UpdateDepartment(DepartmentDto department);
    bool DeleteDepartment(string subdivisionCode);
    bool Save();

    Task<bool> CreateDepartmentAsync(DepartmentDto department);
    Task<bool> UpdateDepartmentAsync(DepartmentDto department);
    Task<bool> DeleteDepartmentAsync(string subdivisionCode);
    Task<bool> SaveAsync();

    #endregion CRUD
}
