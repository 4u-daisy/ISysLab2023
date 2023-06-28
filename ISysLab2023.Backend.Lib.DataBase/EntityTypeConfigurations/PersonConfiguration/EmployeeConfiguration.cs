using ISysLab2023.Backend.Lib.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISysLab2023.Backend.Lib.DataBase.EntityTypeConfigurations.PersonConfiguration;

public class EmployeeConfiguration : BasePersonConfiguration,
    IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .Property(x => x.JobTitle)
            .HasMaxLength(63);
        builder
            .HasIndex(x => x.EmployeeCode)
            .IsUnique(true);
    }
}
