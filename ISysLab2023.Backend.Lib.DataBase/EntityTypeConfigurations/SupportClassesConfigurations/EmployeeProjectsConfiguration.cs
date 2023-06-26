using ISysLab2023.Backend.Lib.Domain.SupportClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISysLab2023.Backend.Lib.DataBase.EntityTypeConfigurations.SupportClassesConfigurations;
public class EmployeeProjectsConfiguration : IEntityTypeConfiguration<EmployeeProjects>
{
    public void Configure(EntityTypeBuilder<EmployeeProjects> builder)
    {
        builder
            .HasAlternateKey(x => x.IdProject + x.IdEmployee);

        builder
            .HasOne(x => x.Employee)
            .WithMany(x => x.EmployeeProjects)
            .HasForeignKey(x => x.IdEmployee);
        builder
            .HasOne(x => x.Project)
            .WithMany(x => x.EmployeeProjects)
            .HasForeignKey(x => x.IdProject);
    }
}
