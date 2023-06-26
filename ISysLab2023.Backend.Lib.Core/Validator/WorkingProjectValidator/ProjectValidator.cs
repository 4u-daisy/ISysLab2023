using FluentValidation;
using ISysLab2023.Backend.Lib.Domain.WorkingProjects;

namespace ISysLab2023.Backend.Lib.Core.Validator.WorkingProjectValidator;
public class ProjectValidator : AbstractValidator<Project>
{
    public ProjectValidator()
    {
        RuleFor(x => x.ProjectCode).NotEmpty()
            .WithMessage("The ProjectCode field cannot be empty");
        RuleFor(x => x.Title).NotEmpty()
            .WithMessage("The Title field cannot be empty");
    }
}
