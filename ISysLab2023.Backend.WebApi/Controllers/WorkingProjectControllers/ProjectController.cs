﻿using ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;
using ISysLab2023.Backend.Lib.Core.ModelDto.PersonDto;
using ISysLab2023.Backend.Lib.Core.ModelDto.WorkingProjectDto;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using Microsoft.AspNetCore.Mvc;

namespace ISysLab2023.Backend.WebApi.Controllers.WorkingProjectControllers;

/// <summary>
/// API Controller for project model
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProjectController : Controller
{
    private readonly IProject _repository;
    private readonly ILogger<Project> _logger;

    /// <summary>
    /// Public constructor to create a controller 
    /// </summary>
    /// <param name="repository">Interface repository</param>
    /// <param name="logger">Interface logger</param>
    public ProjectController(IProject repository,
         ILogger<Project> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    #region BasicQueries

    /// <summary>
    /// Get All Projects on pages. 
    /// </summary>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Project Dto model</returns>
    [HttpGet("GetProjects")]
    public IEnumerable<ProjectDto>? GetProjects(int? pageNumber) =>
        _repository.GetProjects(pageNumber ?? 1);

    /// <summary>
    /// Get All Projects on pages Async. 
    /// </summary>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Project Dto model</returns>
    [HttpGet("GetProjectsAsync")]
    public async Task<IEnumerable<ProjectDto>>? GetProjectsAsync(
        int? pageNumber) =>
        await _repository.GetProjectsAsync(pageNumber ?? 1);

    /// <summary>
    /// Get project by code
    /// </summary>
    /// <param name="code">code project</param>
    /// <returns>Project Dto model</returns>
    [HttpGet("GetProjectByCode/{code}")]
    public ProjectDto? GetProjectByCode(string code) =>
        _repository.GetProjectByCode(code);

    /// <summary>
    /// Get project by code Async
    /// </summary>
    /// <param name="code">code project</param>
    /// <returns>Project Dto model</returns>
    [HttpGet("GetProjectByCodeAsync/{code}")]
    public async Task<ProjectDto>? GetProjectByCodeAsync(
        string code) =>
        await _repository.GetProjectByCodeAsync(code);

    /// <summary>
    ///  Get All Employees of project page by page;
    /// </summary>
    /// <param name="code">code of project</param>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetProjectEmployees/{code}/employees")]
    public IEnumerable<EmployeeDto>? GetProjectEmployees
        (string code, int? pageNumber) =>
        _repository.GetAllEmployeesInProject(code, pageNumber ?? 1);

    /// <summary>
    ///  Get All Employees of project page by page Async
    /// </summary>
    /// <param name="code">code of project</param>
    /// <param name="pageNumber">Page number, by default 1</param>
    /// <returns>List Employee Dto model</returns>
    [HttpGet("GetProjectEmployeesAsync/{code}/employees")]
    public async Task<IEnumerable<EmployeeDto>> GetProjectEmployeesAsync
        (string code, int? pageNumber) =>
        await _repository.GetAllEmployeesInProjectAsync(
            code, pageNumber ?? 1);

    #endregion BasicQueries

    #region CRUD

    /// <summary>
    /// Create a new project
    /// </summary>
    /// <param name="projectDto">dto model Project</param>
    /// <returns>status code</returns>
    [HttpPost("CreateProject")]
    public IActionResult CreateProject(
        [FromBody] ProjectDto projectDto)
    {
        if (projectDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method CreateProject");

        if (!_repository.CreateProject(projectDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }

    /// <summary>
    /// Create a new project Async
    /// </summary>
    /// <param name="projectDto">dto model Project</param>
    /// <returns>status code</returns>
    [HttpPost("CreateProjectAsync")]
    public async Task<IActionResult> CreateProjectAsync(
        [FromBody] ProjectDto projectDto)
    {
        if (projectDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method CreateProject");

        if (!await _repository.CreateProjectAsync(projectDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }

    /// <summary>
    /// Delete project by projectCode
    /// </summary>
    /// <param name="projectCode">code Project</param>
    /// <returns>status code</returns>
    [HttpDelete("DeleteProject")]
    public IActionResult DeleteProject(
        [FromBody] string projectCode)
    {
        if (!_repository.ProjectExists(projectCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method DeleteProject");

        if (!_repository.DeleteProject(projectCode) || !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    /// <summary>
    /// Delete project by projectCode Async
    /// </summary>
    /// <param name="projectCode">code Project</param>
    /// <returns>status code</returns>
    [HttpDelete("DeleteProjectAsync")]
    public async Task<IActionResult> DeleteProjectAsync(
        [FromBody] string projectCode)
    {
        if (!await _repository.ProjectExistsAsync(projectCode))
            return NotFound();

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method DeleteProjectAsync");

        if (!await _repository.DeleteProjectAsync(projectCode) ||
            !ModelState.IsValid)
        {
            ModelState.AddModelError("",
                "Something went wrong deleting institution");
            return BadRequest(ModelState);
        }

        return Ok("Successfully deleted");
    }

    /// <summary>
    /// Update Project model
    /// </summary>
    /// <param name="projectDto">dto model Project</param>
    /// <returns>status code</returns>
    [HttpPut("UpdateProject")]
    public IActionResult UpdateProject(
        [FromBody] ProjectDto projectDto)
    {
        if (projectDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method UpdateProject");

        if (!_repository.UpdateProject(projectDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully updated");
    }

    /// <summary>
    /// Update Project model Async
    /// </summary>
    /// <param name="projectDto">dto model Project</param>
    /// <returns>status code</returns>
    [HttpPut("UpdateProjectAsync")]
    public async Task<IActionResult> UpdateProjectAsync(
        [FromBody] ProjectDto projectDto)
    {
        if (projectDto == null)
            return BadRequest(ModelState);

        _logger.LogInformation($"ModelState {ModelState}, " +
            $"method UpdateProjectAsync");

        if (!await _repository.UpdateProjectAsync(projectDto))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully updated");
    }

    #endregion CRUD
}
