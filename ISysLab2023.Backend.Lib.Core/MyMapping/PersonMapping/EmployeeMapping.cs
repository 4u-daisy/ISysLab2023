using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Domain.Person;

namespace ISysLab2023.Backend.Lib.Core.MyMapping.PersonMapping;
public static class EmployeeMapping
{
    public static Employee? Mapping(EmployeeDto? employeeDto) =>
        employeeDto == null ? null : new Employee()
        {
            Id = employeeDto.Id,
            BirthDay = employeeDto.BirthDay,
            Email = employeeDto.Email,
            EmployeeCode = employeeDto.EmployeeCode,
            JobTitle = employeeDto.JobTitle,
            Name = employeeDto.Name,
            Surname = employeeDto.Surname,
            Patronymic = employeeDto.Patronymic,
            Phone = employeeDto.Phone,
            Status = employeeDto.Status
        };

    public static EmployeeDto? Mapping(Employee? employee) =>
        employee == null ? null : new EmployeeDto()
        {
            Id = employee.Id,
            BirthDay = employee.BirthDay,
            Email = employee.Email,
            EmployeeCode = employee.EmployeeCode,
            JobTitle = employee.JobTitle,
            Name = employee.Name,
            Surname = employee.Surname,
            Patronymic = employee.Patronymic,
            Phone = employee.Phone,
            Status = employee.Status,
            DepartmentCode = employee.Department.SubdivisionCode
        };

    public static IEnumerable<Employee>? Mapping(
        IEnumerable<EmployeeDto>? employeeDto) =>
        employeeDto == null ? null : employeeDto.Select(x => Mapping(x));

    public static IEnumerable<EmployeeDto>? Mapping(
        IEnumerable<Employee> employee) =>
        employee == null ? null : employee.Select(x => Mapping(x));
}
