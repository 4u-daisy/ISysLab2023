namespace ISysLab2023.Backend.Lib.Core.IService.ILoadData;
/// <summary>
/// Interface for importing data in JSON format
/// </summary>
public interface IImportData
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pathToFile"></param>
    /// <returns>result operation, true - successfully, 
    /// otherwise - false</returns>
    public bool ImportData(string pathToFile);

}
