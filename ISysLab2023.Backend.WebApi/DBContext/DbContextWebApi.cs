using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.WebApi.Service;
using Microsoft.EntityFrameworkCore;

namespace ISysLab2023.Backend.WebApi.DBContext;

public class DbContextWebApi : DataBaseContext
{
    public static readonly ILoggerFactory loggerFactory =
    LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Information);
    });

    public DbContextWebApi() : base() { }

    public DbContextWebApi(DbContextOptions<DataBaseContext> options)
        : base(options) { }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(loggerFactory);
        optionsBuilder.UseMySQL(Config.ConnectionString);
    }
}
