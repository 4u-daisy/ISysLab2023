using ISysLab2023.Backend.Lib.Core.IService.ILoadData;
using ISysLab2023.Backend.Lib.DataBase.DBContext;

namespace ISysLab2023.Backend.Lib.Core.Repository.LoadDataRepository;
public class ExportDataRepository : IExportDataJson
{
    private readonly DataBaseContext _dbContext;
    public ExportDataRepository(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool ExportDataJson(string pathToFile)
    {
        using (var fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
        {
            //JsonSerializer.Serialize(fs, elems);
        }
        return true;

    }
}
