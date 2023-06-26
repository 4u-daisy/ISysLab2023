﻿using ISysLab2023.Backend.Lib.Domain.Person;
using System.ComponentModel.DataAnnotations;

namespace ISysLab2023.Backend.Lib.Domain.Organization;

/// <summary>
/// Division of the organization
/// </summary>
public class Department
{
    /// <summary>
    /// Autogenerated primary key in string format. Represents Guid
    /// </summary>
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Name of department
    /// </summary>
    public string Title { get; set; } = String.Empty;

    /// <summary>
    /// Subdivision code
    /// </summary>
    public string SubdivisionCode { get; set; } = String.Empty;

    /// <summary>
    /// Department employees
    /// </summary>
    public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
}
