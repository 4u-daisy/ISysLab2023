using ISysLab2023.Backend.Lib.Core.IService.ILoadData;
using Microsoft.AspNetCore.Mvc;

namespace ISysLab2023.Backend.WebApi.Controllers.LoadDataControllers;

/// <summary>
/// Imports the database from a file
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ImportDataController : Controller
{
    private readonly IImportData _repository;

    /// <summary>
    /// Public constructor to create a controller 
    /// </summary>
    /// <param name="repository">Interface repository</param>
    public ImportDataController(IImportData repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// importing data from Json file
    /// </summary>
    /// <param name="pathToFile">path to the file with source data</param>
    /// <returns>StatusCode 500 - fail import
    ///          StatusCode 200 - successfully import</returns>
    [HttpGet("ImportDataFromJson")]
    public IActionResult ImportDataFromJson(string pathToFile)
    {
        //_logger.LogInformation($"path to file {pathToFile}, "
        //    + $"method ImportDataFromJson");

        if (!_repository.ImportData(pathToFile))
        {
            return StatusCode(500);
        }
        return StatusCode(200);

    }


}
