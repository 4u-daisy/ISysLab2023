using ISysLab2023.Backend.Lib.Core.IService.IOrganization;
using ISysLab2023.Backend.Lib.Core.ModelDto.OrganizationDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Domain.Organization;
using Microsoft.AspNetCore.Mvc;

namespace ISysLab2023.Backend.WebApi.Controllers.OrganizationControllers;

/// <summary>
/// API Controller for department model
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DepartmentController : Controller
{
    private readonly IDepartment _repository;
    private readonly ILogger<Department> _logger;

    /// <summary>
    /// Public constructor to create a controller 
    /// </summary>
    /// <param name="repository">Interface repository</param>
    /// <param name="logger">Interface logger</param>
    public DepartmentController(IDepartment repository,
        ILogger<Department> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    #region BasicQueries

    /// <summary>
    /// Get All Departments on pages. 
    /// </summary>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Department Dto model</returns>
    [HttpGet("GetDepartments")]
    public IEnumerable<DepartmentDto>? GetDepartments(
        int? pageNumber) =>
        _repository.GetDepartments(pageNumber ?? 1);

    /// <summary>
    /// Get All Departments Async on pages. 
    /// </summary>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Department Dto model</returns>
    [HttpGet("GetDepartmentsAsync")]
    public async Task<IEnumerable<DepartmentDto>>? GetDepartmentsAsync(
        int? pageNumber) =>
        await _repository.GetDepartmentsAsync(pageNumber ?? 1);

    /// <summary>
    /// Get department by code
    /// </summary>
    /// <param name="code">code depatment</param>
    /// <returns>Department Dto model</returns>
    [HttpGet("GetDepartmentByCode/{code}")]
    public DepartmentDto? GetDepartmentByCode(string code) =>
        _repository.GetDepartmentByCode(code);


    /// <summary>
    /// Get department by code Async
    /// </summary>
    /// <param name="code">code department</param>
    /// <returns>Department Dto model</returns>
    [HttpGet("GetDepartmentByCodeAsync/{code}")]
    public async Task<DepartmentDto>? GetDepartmentByCodeAsync(
        string code) =>
        await _repository.GetDepartmentByCodeAsync(code);

    /// <summary>
    ///  Get All Employees of department page by page;
    /// </summary>
    /// <param name="code">code of department</param>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetDepartmentEmployees/{code}/employees")]
    public IEnumerable<EmployeeDto>? GetDepartmentEmployees
        (string code, int? pageNumber) =>
        _repository.GetEmployees(code, pageNumber ?? 1);

    /// <summary>
    ///  Get All Employees of department page by page Async;
    /// </summary>
    /// <param name="code">code of department</param>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetDepartmentEmployeesAsync/{code}/employees")]
    public async Task<IEnumerable<EmployeeDto>>? GetDepartmentEmployeesAsync(
        string code, int? pageNumber) =>
        await _repository.GetEmployeesAsync(code, pageNumber ?? 1);

    #endregion BasicQueries

    #region CRUD
    /// <summary>
    /// Create a new department
    /// </summary>
    /// <param name="departmentDto">dto model Department</param>
    /// <returns>status code</returns>
    [HttpPost("CreateDepartment")]
    public IActionResult CreateDepartment(
        [FromBody] DepartmentDto departmentDto)
    {
        if (departmentDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method CreateDepartment");

        if (!_repository.CreateDepartment(departmentDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }

    /// <summary>
    /// Create a new department Async
    /// </summary>
    /// <param name="departmentDto">dto model Department</param>
    /// <returns>status code</returns>
    [HttpPost("CreateDepartmentAsync")]
    public async Task<IActionResult> CreateDepartmentAsync(
        [FromBody] DepartmentDto departmentDto)
    {
        if (departmentDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method CreateDepartmentAsync");

        if (!await _repository.CreateDepartmentAsync(departmentDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }

    /// <summary>
    /// Delete department by subdivisionCode
    /// </summary>
    /// <param name="subdivisionCode">code department</param>
    /// <returns>status code</returns>
    [HttpDelete("DeleteDepartment")]
    public IActionResult DeleteDepartment(
        [FromBody] string subdivisionCode)
    {
        if (!_repository.DepartmentExists(subdivisionCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method DeleteDepartment");

        if (!_repository.DeleteDepartment(subdivisionCode)
            || !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    /// <summary>
    /// Delete department by subdivisionCodeAsync
    /// </summary>
    /// <param name="subdivisionCode">code department</param>
    /// <returns>status code</returns>
    [HttpDelete("DeleteDepartmentAsync")]
    public async Task<IActionResult> DeleteDepartmentAsync(
        [FromBody] string subdivisionCode)
    {
        if (!await _repository.DepartmentExistsAsync(subdivisionCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method DeleteDepartmentAsync");

        if (!await _repository.DeleteDepartmentAsync(subdivisionCode) ||
            !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    /// <summary>
    /// Update Department model
    /// </summary>
    /// <param name="departmentDto">dto model Department</param>
    /// <returns>status code</returns>
    [HttpPut("UpdateDepartment")]
    public IActionResult UpdateDepartment(
        [FromBody] DepartmentDto departmentDto)
    {
        if (departmentDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method UpdateDepartment");

        if (!_repository.UpdateDepartment(departmentDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully updated");
    }

    /// <summary>
    /// Update Department model Async
    /// </summary>
    /// <param name="departmentDto">dto model Department</param>
    /// <returns>status code</returns>
    [HttpPut("UpdateDepartmentAsync")]
    public async Task<IActionResult> UpdateDepartmentAsync(
        [FromBody] DepartmentDto departmentDto)
    {
        if (departmentDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method UpdateDepartmentAsync");

        if (!await _repository.UpdateDepartmentAsync(departmentDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully updated");
    }

    #endregion CRUD

}
