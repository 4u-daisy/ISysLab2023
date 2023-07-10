using ISysLab2023.Backend.Lib.Core.IService.ILoadData;
using Microsoft.AspNetCore.Mvc;

namespace ISysLab2023.Backend.WebApi.Controllers.LoadDataControllers;

/// <summary>
/// Exporting data to JSON file
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ExportDataController : Controller
{

    private readonly IExportDataJson _repository;

    /// <summary>
    /// Public constructor to create a controller 
    /// </summary>
    /// <param name="repository">Interface repository</param>
    public ExportDataController(IExportDataJson repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// exporting data to Json file
    /// </summary>
    /// <param name="pathToFile">path to the file to which
    /// data should be exported</param>
    /// <returns>StatusCode 500 - fail export
    ///          StatusCode 200 - successfully export</returns>
    [HttpGet("ExportDataToJson")]
    public IActionResult ExportDataToJson(string pathToFile)
    {
        //_logger.LogInformation($"path to file {pathToFile}, "
        //    + $"method ExportDataToJson");

        if (!_repository.ExportDataJson(pathToFile))
        {
            return StatusCode(500);
        }
        return StatusCode(200);

    }


}
