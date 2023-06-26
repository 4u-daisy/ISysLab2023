using ISysLab2023.Backend.Lib.Domain.WorkingProjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISysLab2023.Backend.Lib.DataBase.EntityTypeConfigurations.WorkingProjectConfigurations;
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .IsRequired();
        builder
            .Property(x => x.Title)
            .HasMaxLength(63);
        builder
            .Property(x => x.ProjectCode)
            .HasMaxLength(63);
    }
}
