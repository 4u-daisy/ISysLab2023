using ISysLab2023.Backend.Lib.Core.IService.IOrganization;
using ISysLab2023.Backend.Lib.Core.IService.IPerson;
using ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
using ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;
using ISysLab2023.Backend.Lib.Core.Repository.OrganizationRepository;
using ISysLab2023.Backend.Lib.Core.Repository.PersonRepository;
using ISysLab2023.Backend.Lib.Core.Repository.SupportClassesRepository;
using ISysLab2023.Backend.Lib.Core.Repository.WorkingProjectRepository;
using ISysLab2023.Backend.WebApi.DBContext;
using ISysLab2023.Backend.WebApi.Service;
using Microsoft.EntityFrameworkCore;

using FluentValidation;
using FluentValidation.AspNetCore;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = builder.Configuration;

configuration.GetSection(Config.Project).Bind(new Config());

builder.Services.AddDbContextFactory<DataBaseContext>(optionsBuilder
    => optionsBuilder.UseMySQL(Config.ConnectionString));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddScoped<IDepartment, DepartmentRepository>()
    .AddScoped<IEmployee, EmployeeRepository>()
    .AddScoped<IProject, ProjectRepository>()
    .AddScoped<IEmployeeProjects, EmployeeProjectsRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddFluentValidation()
    .AddValidatorsFromAssembly(typeof(DepartmentRepository).Assembly)
    .AddValidatorsFromAssembly(typeof(EmployeeRepository).Assembly)
    .AddValidatorsFromAssembly(typeof(ProjectRepository).Assembly)
    .AddValidatorsFromAssembly(typeof(EmployeeProjectsRepository).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<DataBaseContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
