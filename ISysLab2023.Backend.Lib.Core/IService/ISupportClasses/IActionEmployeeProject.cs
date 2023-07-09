using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.SupportClassDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;

namespace ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
public interface IActionEmployeeProject
{
    #region Show
    public ICollection<EmployeeProjectDto>? GetAllEmployeeProject(
        int page = 1);
    public Task<ICollection<EmployeeProjectDto>>? GetAllEmployeeProjectAsync(
        int page = 1);
    #endregion Show

    #region CheckExists
    bool ParticipatesInProject(string projectCode, int codeEmployee);
    Task<bool> ParticipatesInProjectAsync(
        string projectCode, int codeEmployee);
    #endregion CheckExists

    #region GetAllEmployeesInProject
    ICollection<EmployeeDto>? GetAllEmployeesInProject(
        string projectCode, int page = 1);
    Task<ICollection<EmployeeDto>> GetAllEmployeesInProjectAsync(
        string projectCode, int page = 1);
    #endregion GetAllEmployeesInProject

    #region GetAllProjectsEmployee
    ICollection<ProjectDto>? GetAllProjectsEmployee(
        int employeeCode, int page = 1);
    Task<ICollection<ProjectDto>>? GetAllProjectsEmployeeAsync(
        int employeeCode, int page = 1);
    #endregion GetAllProjectsEmployee

    #region Save
    bool Save();
    Task<bool> SaveAsync();
    #endregion Save

}