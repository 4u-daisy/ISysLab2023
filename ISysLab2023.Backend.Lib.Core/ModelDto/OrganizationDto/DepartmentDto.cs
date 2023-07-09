namespace ISysLab2023.Backend.Lib.Core.ModelDto.OrganizationDto;
/// <summary>
/// create department dto model
/// </summary>
public class DepartmentDto
{
    public Guid Id { get; set; } = new();
    /// <summary>
    /// Name of department
    /// </summary>
    public string Title { get; set; } = String.Empty;

    /// <summary>
    /// Subdivision code
    /// </summary>
    public string SubdivisionCode { get; set; } = String.Empty;
}