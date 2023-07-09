using ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
using ISysLab2023.Backend.Lib.Core.ModelDto.SupportClassDto;
using ISysLab2023.Backend.Lib.Domain.SupportClasses;
using Microsoft.AspNetCore.Mvc;

namespace ISysLab2023.Backend.WebApi.Controllers.SupportClassesControllers;

/// <summary>
/// API Controller for employeeProject model
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeProjectController : Controller
{
    private readonly IEmployeeProject _repository;
    private readonly ILogger<EmployeeProjects> _logger;

    /// <summary>
    /// Public constructor to create a controller 
    /// </summary>
    /// <param name="repository">Interface repository</param>
    /// <param name="logger">Interface logger</param>
    public EmployeeProjectController(IEmployeeProject repository,
        ILogger<EmployeeProjects> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    #region AddEmployeeInProject

    /// <summary>
    /// Add Employee In Project
    /// </summary>
    /// <param name="employeeProjectDto">dto model EmployeeProject</param>
    /// <returns>status code</returns>
    [HttpPost("AddEmployeeInProject")]
    public IActionResult AddEmployeeInProject(
        [FromBody] EmployeeProjectDto employeeProjectDto)
    {
        if (employeeProjectDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method AddEmployeeInProject");

        if (!_repository.AddEmployeeInProject(
            employeeProjectDto.ProjectCode, employeeProjectDto.EmployeeCode))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully added");
    }

    /// <summary>
    /// Add Employee In Project Async
    /// </summary>
    /// <param name="employeeProjectDto">dto model EmployeeProject</param>
    /// <returns>status code</returns>
    [HttpPost("AddEmployeeInProjectAsync")]
    public async Task<IActionResult> AddEmployeeInProjectAsync(
        [FromBody] EmployeeProjectDto employeeProjectDto)
    {
        if (employeeProjectDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method AddEmployeeInProjectAsync");

        if (!await _repository.AddEmployeeInProjectAsync(
            employeeProjectDto.ProjectCode, employeeProjectDto.EmployeeCode))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully added");
    }

    #endregion AddEmployeeInProject

    #region RemoveEmployeeFromProject

    /// <summary>
    /// Remove Employee From Project
    /// </summary>
    /// <param name="employeeProjectCodeDto">employee project dto</param>
    /// <returns>status code</returns>
    [HttpDelete("RemoveEmployeeFromProject")]
    public IActionResult RemoveEmployeeFromProject(
        [FromBody] EmployeeProjectDto employeeProjectCodeDto)
    {
        if (!_repository.ParticipatesInProject(
            employeeProjectCodeDto.ProjectCode,
            employeeProjectCodeDto.EmployeeCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method RemoveEmployeeFromProject");

        if (!_repository.RemoveEmployeeFromProject(
            employeeProjectCodeDto.ProjectCode,
            employeeProjectCodeDto.EmployeeCode) ||
            !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    /// <summary>
    /// Remove Employee From Project Async
    /// </summary>
    /// <param name="employeeProjectCodeDto">employee project dto</param>
    /// <returns>status code</returns>
    [HttpDelete("RemoveEmployeeFromProjectAsync")]
    public async Task<IActionResult> RemoveEmployeeFromProjectAsync(
        [FromBody] EmployeeProjectDto employeeProjectCodeDto)
    {
        if (!await _repository.ParticipatesInProjectAsync(
            employeeProjectCodeDto.ProjectCode,
            employeeProjectCodeDto.EmployeeCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method RemoveEmployeeFromProjectAsync");

        if (!await _repository.RemoveEmployeeFromProjectAsync(
            employeeProjectCodeDto.ProjectCode,
            employeeProjectCodeDto.EmployeeCode) ||
            !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    #endregion RemoveEmployeeFromProject

    #region ShowEmployeeProjects

    /// <summary>
    /// Get all EmployeeProjectDto
    /// </summary>
    /// <param name="page">Page number, by default 1</param>
    /// <returns>All EmployeeProjectDto</returns>
    [HttpGet("ShowEmployeeProjects")]
    public IEnumerable<EmployeeProjectDto>? ShowEmployeeProjects(
        int? page = 1) =>
        _repository.GetAllEmployeeProject(page ?? 1);

    /// <summary>
    /// Get all EmployeeProjectDto Async
    /// </summary>
    /// <param name="page">Page number, by default 1</param>
    /// <returns>All EmployeeProjectDto</returns>
    [HttpGet("ShowEmployeeProjectsAsync")]
    public async Task<IEnumerable<EmployeeProjectDto>>? ShowEmployeeProjectsAsync(
    int? page = 1) =>
    await _repository.GetAllEmployeeProjectAsync(page ?? 1);


    #endregion ShowEmployeeProjects

}
