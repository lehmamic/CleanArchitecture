using CleanArchitecture.Application.Projects.Commands.CreateProject;
using FluentValidation;
using JetBrains.Annotations;

namespace CleanArchitecture.Application.Projects.Commands.UpdateProject;

[UsedImplicitly]
public class UpdateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
