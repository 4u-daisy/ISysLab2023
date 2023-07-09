namespace ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;
public class ProjectDto
{
    public Guid Id { get; set; } = new();
    /// <summary>
    /// Name of department
    /// </summary>
    public string Title { get; set; } = String.Empty;

    /// <summary>
    /// Project code
    /// </summary>
    public string ProjectCode { get; set; } = String.Empty;
}
