namespace ISysLab2023.Backend.Lib.Core.IService.ILoadData;
/// <summary>
/// Interface for exporting data in JSON format
/// </summary>
public interface IExportDataJson
{
    /// <summary>
    /// export data json format
    /// </summary>
    /// <param name="pathToFile">path to file from 
    /// which want to import data</param>
    /// <returns>result operation, true - successfully, 
    /// otherwise - false</returns>
    public bool ExportDataJson(string pathToFile);
}
