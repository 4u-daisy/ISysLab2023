using ISysLab2023.Backend.Lib.Core.IService.IOrganization;
using ISysLab2023.Backend.Lib.Core.IService.IPerson;
using ISysLab2023.Backend.Lib.Core.IService.ISupportClasses;
using ISysLab2023.Backend.Lib.Core.IService.IWorkingProject;
using ISysLab2023.Backend.Lib.Core.Repository.OrganizationRepository;
using ISysLab2023.Backend.Lib.Core.Repository.PersonRepository;
using ISysLab2023.Backend.Lib.Core.Repository.SupportClassesRepository;
using ISysLab2023.Backend.Lib.Core.Repository.WorkingProjectRepository;
using ISysLab2023.Backend.Lib.DataBase.DBContext;
using ISysLab2023.Backend.WebApi.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// TODO need to do
// 1. secrets
// 2. normal summary
// 3. data annotation for controllers (with result codes)
// 5. go to postgre

// валидация на контроллеры


var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

configuration.GetSection(Config.Project).Bind(new Config());

builder.Services.AddDbContextFactory<DataBaseContext>(optionsBuilder
    => optionsBuilder.UseMySQL(Config.ConnectionString));

builder.Services.AddControllers();

builder.Services
    .AddScoped<IDepartment, DepartmentRepository>()
    .AddScoped<IEmployee, EmployeeRepository>()
    .AddScoped<IProject, ProjectRepository>()
    .AddScoped<IEmployeeProject, EmployeeProjectRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});


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
