using ISysLab2023.Backend.Lib.Core.IService.ILoadData;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.Lib.Domain.Person;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ISysLab2023.Backend.Lib.Core.Repository.LoadDataRepository;
public class ImportDataRepositiry : IImportData
{
    private readonly DataBaseContext _dbContext;
    public ImportDataRepositiry(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool ImportData(string pathToFile)
    {
        using (var fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
        {
            var collection = JsonSerializer.Deserialize<ICollection<Employee>>(fs);
            if (collection == null)
                return false;

            _dbContext.Employees.AddRange(collection);
            _dbContext.SaveChanges();
        }

        return true;
    }
}
