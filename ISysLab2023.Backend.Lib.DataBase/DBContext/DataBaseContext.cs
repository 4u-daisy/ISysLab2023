using ISysLab2023.Backend.Lib.DataBase.EntityTypeConfigurations.OrganizationConfigurations;
using ISysLab2023.Backend.Lib.DataBase.EntityTypeConfigurations.PersonConfiguration;
using ISysLab2023.Backend.Lib.DataBase.EntityTypeConfigurations.SupportClassesConfigurations;
using ISysLab2023.Backend.Lib.DataBase.EntityTypeConfigurations.WorkingProjectConfigurations;
using ISysLab2023.Backend.Lib.Domain.Organization;
using ISysLab2023.Backend.Lib.Domain.Person;
using ISysLab2023.Backend.Lib.Domain.SupportClasses;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using Microsoft.EntityFrameworkCore;

namespace ISysLab2023.Backend.Lib.DataBase.DBContext;

public class DataBaseContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProjects> EmployeeProjects { get; set; }

    public DataBaseContext() : base() { }
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeProjectsConfiguration).Assembly);
    }
}
