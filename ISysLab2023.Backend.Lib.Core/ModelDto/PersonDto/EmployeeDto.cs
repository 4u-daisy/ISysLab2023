using ISysLab2023.Backend.Lib.Domain.Person;
using System.ComponentModel.DataAnnotations;

namespace ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
public class EmployeeDto
{
    public Guid Id { get; set; } = new();
    /// <summary>
    /// Name of the person
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Surname of the person
    /// </summary>
    public string? Surname { get; set; }
    /// <summary>
    /// Patronymic of the person
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Birthday. Format DD.MM.YYYY
    /// </summary>
    public DateOnly? BirthDay { get; set; }

    /// <summary>
    /// Email. Format lalala@lala.la
    /// </summary>
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public string? Email { get; set; }

    /// <summary>
    /// Phone. Format @[0-9]{11}
    /// </summary>
    [RegularExpression(@"[0-9]{10}")]
    public string? Phone { get; set; }


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
    /// EmployeeCode of the employee's supervisor
    /// </summary>
    public int HeadManagerCode { get; set; }
    /// <summary>
    /// Code of the department in which the person works
    /// </summary>
    public string DepartmentCode { get; set; } = String.Empty;

    /// <summary>
    /// Employee's job status
    /// </summary>
    public Status Status { get; set; } = Status.Active;
}
