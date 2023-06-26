using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;

namespace ISysLab2023.Backend.Lib.Domain.SupportClasses;

/// <summary>
/// A class that implements a many-to-many relationship 
/// for the Employee and Project classes
/// </summary>
public class EmployeeProjects
{
    /// <summary>
    /// Id of Employee
    /// </summary>
    public string IdEmployee { get; set; } = String.Empty;
    /// <summary>
    /// Link of Employee
    /// </summary>
    public Employee? Employee { get; set; }

    /// <summary>
    /// Id of Project
    /// </summary>
    public string IdProject { get; set; } = String.Empty;
    /// <summary>
    /// Link of Project
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// The key for the supporting table, 
    /// because without it it does not work for me :(
    /// </summary>
    public string Key { get; set; } = new Guid().ToString();
}
