using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.Lib.Domain.SupportClasses;

namespace ISysLab2023.Backend.Lib.Domain.Person;

/// <summary>
/// Employee's job status
/// </summary>
public enum Status
{
    /// <summary>
    /// Actively working
    /// </summary>
    Active,
    /// <summary>
    /// On vacation
    /// </summary>
    Vacation,
    /// <summary>
    /// Employee is fired
    /// </summary>
    Fired,
    /// <summary>
    /// An employee is sick.
    /// </summary>
    SickLeave,
    /// <summary>
    /// The employee is on maternity leave
    /// </summary>
    MaternityLeave
}

/// <summary>
/// The class is a description of a employee.
/// </summary>
public class Employee : BasePerson
{
    /// <summary>
    /// Job title
    /// </summary>
    public string JobTitle { get; set; } = String.Empty;

    /// <summary>
    /// Internal employee code. It is unique. 
    /// It is required to identify a specific employee.
    /// The default value is -1
    /// </summary>
    public int EmployeeCode { get; set; } = -1;

    /// <summary>
    /// Id of the employee's supervisor
    /// </summary>
    public string? IdHeadManager { get; set; }
    /// <summary>
    /// Link of the employee's supervisor
    /// </summary>
    public Employee? HeadManager { get; set; }

    /// <summary>
    /// Id of the department in which the person works
    /// </summary>
    public string IdDepartment { get; set; } = String.Empty;
    /// <summary>
    /// Link of the department in which the person works
    /// </summary>
    public Department Department { get; set; } = new();

    /// <summary>
    /// Employee's job status
    /// </summary>
    public Status Status { get; set; } = Status.Active;

    /// <summary>
    /// A field that implements a many-to-many relationship 
    /// with the Project class
    /// </summary>
    public ICollection<EmployeeProjects>? EmployeeProjects { get; set; } 
        = new List<EmployeeProjects>();
}
