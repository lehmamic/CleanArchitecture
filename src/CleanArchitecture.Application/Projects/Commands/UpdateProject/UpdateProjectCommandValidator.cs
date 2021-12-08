using CleanArchitecture.Application.Projects.Commands.CreateProject;
using FluentValidation;

namespace CleanArchitecture.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
