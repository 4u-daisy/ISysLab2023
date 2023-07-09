using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;

namespace ISysLab2023.Backend.Lib.Core.MyMapping.WorkingProjectsMapping;
public static class ProjectMapping
{
    public static Project? Mapping(ProjectDto? projectDto) =>
        projectDto == null ? null : new Project()
        {
            Id = projectDto.Id,
            ProjectCode = projectDto.ProjectCode,
            Title = projectDto.Title
        };
    public static ProjectDto? Mapping(Project? project) =>
    project == null ? null : new ProjectDto()
    {
        Id = project.Id,
        ProjectCode = project.ProjectCode,
        Title = project.Title
    };

    public static IEnumerable<Project>? Mapping(
        IEnumerable<ProjectDto>? projectDto) =>
        projectDto == null ? null : projectDto.Select(x => Mapping(x));

    public static IEnumerable<ProjectDto>? Mapping(
        IEnumerable<Project>? projectDto) =>
        projectDto == null ? null : projectDto.Select(x => Mapping(x));

}
