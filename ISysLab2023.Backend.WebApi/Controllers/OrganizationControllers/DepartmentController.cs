using AutoMapper;
using ISysLab2023.Backend.Lib.Core.IService.IOrganization;
using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.WebApi.ModelsDto.OrganizationDto;
using ISysLab2023.Backend.WebApi.Service;
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
    private readonly IMapper _mapper;
    private readonly ILogger<Department> _logger;

    /// <summary>
    /// Public constructor to create a controller 
    /// </summary>
    /// <param name="repository">Interface repository</param>
    /// <param name="mapper">Interface mapper</param>
    /// <param name="logger">Interface logger</param>
    public DepartmentController(IDepartment repository,
        IMapper mapper, ILogger<Department> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    #region BasicQueries

    /// <summary>
    /// Get All Departments
    /// </summary>
    /// <returns>List of Departments</returns>
    [HttpGet("GetDepartments")]
    public IEnumerable<DepartmentDto> GetDepartments(int? pageNumber) =>
        PagedList<DepartmentDto>.Create(
            _mapper.Map<List<DepartmentDto>>(_repository.GetDepartments()),
            pageNumber ?? 1);

    /// <summary>
    /// Get All Departments Async
    /// </summary>
    /// <returns>List of Departments</returns>
    [HttpGet("GetDepartmentsAsync")]
    public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync() =>
        _mapper.Map<List<DepartmentDto>>(await _repository.GetDepartmentsAsync());

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

        var department = _mapper.Map<Department>(departmentDto);
        if (!_repository.CreateDepartment(department))
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

        var department = _mapper.Map<Department>(departmentDto);
        if (!await _repository.CreateDepartmentAsync(department))
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

        if (!_repository.DeleteDepartment(subdivisionCode) || !ModelState.IsValid)
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
            $"method DeleteDepartment");

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

        var department = _mapper.Map<Department>(departmentDto);
        if (!_repository.UpdateDepartment(department))
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
            $"method UpdateDepartment");

        var department = _mapper.Map<Department>(departmentDto);
        if (!await _repository.UpdateDepartmentAsync(department))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully updated");
    }

    #endregion CRUD

}
