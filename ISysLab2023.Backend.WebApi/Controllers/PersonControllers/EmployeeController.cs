using ISysLab2023.Backend.Lib.Core.IService.IPerson;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Domain.Person;
using Microsoft.AspNetCore.Mvc;

namespace ISysLab2023.Backend.WebApi.Controllers.PersonControllers;

/// <summary>
/// API Controller for employee model
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : Controller
{
    private readonly IEmployee _repository;
    private readonly ILogger<Employee> _logger;

    /// <summary>
    /// Public constructor to create a controller 
    /// </summary>
    /// <param name="repository">Interface repository</param>
    /// <param name="logger">Interface logger</param>
    public EmployeeController(IEmployee repository,
        ILogger<Lib.Domain.Person.Employee> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    #region BasicQueries

    /// <summary>
    /// Get All Employee on pages. 
    /// </summary>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetEmployees")]
    public IEnumerable<EmployeeDto>? GetEmployees(
        int? pageNumber) =>
        _repository.GetEmployees(pageNumber ?? 1);


    /// <summary>
    /// Get All Employees Async on pages. 
    /// </summary>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetEmployeesAsync")]
    public async Task<IEnumerable<EmployeeDto>>? GetEmployeesAsync(
        int? pageNumber) =>
        await _repository.GetEmployeesAsync(pageNumber ?? 1);

    /// <summary>
    /// Get employee by code
    /// </summary>
    /// <param name="code">code employee</param>
    /// <returns>Employee Dto model</returns>
    [HttpGet("GetEmployeesByCode/{code}")]
    public EmployeeDto? GetEmployeesByCode(int code) =>
        _repository.GetEmployeeByCode(code);

    /// <summary>
    /// Get employee by code Async
    /// </summary>
    /// <param name="code">code employee</param>
    /// <returns>Employee Dto model</returns>
    [HttpGet("GetEmployeesByCodeAsync/{code}")]
    public async Task<EmployeeDto>? GetEmployeesByCodeAsync(
        int code) =>
        await _repository.GetEmployeeByCodeAsync(code);

    /// <summary>
    /// Get all subordinate employees
    /// </summary>
    /// <param name="code">code employee</param>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetSubEmployees/{code}/subordinates")]
    public IEnumerable<EmployeeDto>? GetSubEmployees(
        int code, int? pageNumber) =>
        _repository.GetSubEmployees(code, pageNumber ?? 1);

    /// <summary>
    /// Get all subordinate employees Async
    /// </summary>
    /// <param name="code">code employee</param>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetSubEmployeesAsync/{code}/subordinates")]
    public async Task<IEnumerable<EmployeeDto>>? GetSubEmployeesAsync(
        int code, int? pageNumber) =>
        await _repository.GetSubEmployeesAsync(code, pageNumber ?? 1);

    #endregion BasicQueries

    #region CRUD

    /// <summary>
    /// Create a new Employee
    /// </summary>
    /// <param name="employeeDto">dto model Employee</param>
    /// <returns>status code</returns>
    [HttpPost("CreateEmployee")]
    public IActionResult CreateEmployee(
        [FromBody] EmployeeDto employeeDto)
    {
        if (employeeDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method CreateEmployee");

        if (!_repository.CreateEmployee(employeeDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }

    /// <summary>
    /// Create a new employee Async
    /// </summary>
    /// <param name="employeeDto">dto model Employee</param>
    /// <returns>status code</returns>
    [HttpPost("CreateEmployeeAsync")]
    public async Task<IActionResult> CreateEmployeeAsync(
        [FromBody] EmployeeDto employeeDto)
    {
        if (employeeDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method CreateEmployeeAsync");

        if (!await _repository.CreateEmployeeAsync(employeeDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }

    /// <summary>
    /// Delete employee by code
    /// </summary>
    /// <param name="employeeCode">code employee</param>
    /// <returns>status code</returns>
    [HttpDelete("DeleteEmployee")]
    public IActionResult DeleteEmployee(
        [FromBody] int employeeCode)
    {
        if (!_repository.EmployeeExists(employeeCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method DeleteEmployee");

        if (!_repository.DeleteEmployee(employeeCode) || !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    /// <summary>
    /// Delete employee by code Async
    /// </summary>
    /// <param name="employeeCode">code employee</param>
    /// <returns>status code</returns>
    [HttpDelete("DeleteEmployeeAsync")]
    public async Task<IActionResult> DeleteEmployeeAsync(
        [FromBody] int employeeCode)
    {
        if (!await _repository.EmployeeExistsAsync(employeeCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method DeleteEmployeeAsync");

        if (!await _repository.DeleteEmployeeAsync(employeeCode) ||
            !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    /// <summary>
    /// Update Employee model
    /// </summary>
    /// <param name="employeeDto">dto model employee</param>
    /// <returns>status code</returns>
    [HttpPut("UpdateEmployee")]
    public IActionResult UpdateEmployee(
        [FromBody] EmployeeDto employeeDto)
    {
        if (employeeDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method UpdateEmployee");

        if (!_repository.UpdateEmployee(employeeDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully updated");
    }

    /// <summary>
    /// Update Employee model Async
    /// </summary>
    /// <param name="employeeDto">dto model Employee</param>
    /// <returns>status code</returns>
    [HttpPut("UpdateEmployeeAsync")]
    public async Task<IActionResult> UpdateEmployeeAsync(
        [FromBody] EmployeeDto employeeDto)
    {
        if (employeeDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method UpdateEmployeeAsync");

        if (!await _repository.UpdateEmployeeAsync(employeeDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully updated");
    }
    #endregion CRUD

}
