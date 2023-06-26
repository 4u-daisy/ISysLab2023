using FluentValidation;
using ISysLab2023.Backend.Lib.Domain.Organization;

namespace ISysLab2023.Backend.Lib.Core.Validator.OrganizationValidator;
public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.SubdivisionCode).NotEmpty()
            .WithMessage("The SubdivisionCode field cannot be empty");
        RuleFor(x => x.Title).NotEmpty()
            .WithMessage("The Title field cannot be empty");
    }
}
