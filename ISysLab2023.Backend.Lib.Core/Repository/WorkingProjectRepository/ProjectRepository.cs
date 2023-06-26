using ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;
using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;

namespace ISysLab2023.Backend.Lib.Core.Repository.WorkingProjectRepository;
public class ProjectRepository : IProject
{
    public ICollection<Employee>? GetAllEmployeesInProject(string projectCode)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Employee>> GetAllEmployeesInProjectAsync(string projectCode)
    {
        throw new NotImplementedException();
    }

    public Project? GetProjectByCode(string code)
    {
        throw new NotImplementedException();
    }

    public Task<Project>? GetProjectByCodeAsync(string code)
    {
        throw new NotImplementedException();
    }

    public ICollection<Project>? GetProjects()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Project>>? GetProjectsAsync()
    {
        throw new NotImplementedException();
    }

    public bool ParticipatesInProject(string projectCode, int codeEmployee)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ParticipatesInProjectAsync(string projectCode, int codeEmployee)
    {
        throw new NotImplementedException();
    }

    public bool ProjectExists(string code)
    {
        throw new NotImplementedException();
    }

    public bool ProjectExistsAsync(string code)
    {
        throw new NotImplementedException();
    }
}
