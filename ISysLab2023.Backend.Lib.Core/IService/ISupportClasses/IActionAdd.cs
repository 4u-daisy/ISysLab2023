namespace ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
public interface IActionAdd
{
    #region AddEmployeeInProject
    bool AddEmployeeInProject(string projectCode, int employeeCode);
    Task<bool> AddEmployeeInProjectAsync(string projectCode, int employeeCode);
    #endregion AddEmployeeInProject
}
