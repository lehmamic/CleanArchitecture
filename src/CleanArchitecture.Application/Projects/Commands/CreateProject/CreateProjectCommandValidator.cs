using FluentValidation;
using JetBrains.Annotations;

namespace CleanArchitecture.Application.Projects.Commands.CreateProject;

[UsedImplicitly]
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
