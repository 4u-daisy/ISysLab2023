using FluentValidation;
using ISysLab2023.Backend.Lib.Domain.Person;

namespace ISysLab2023.Backend.Lib.Core.Validator.PersonValidator;
public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.JobTitle).NotEmpty()
            .WithMessage("The CodeEmployee field cannot be empty");
    }
}
