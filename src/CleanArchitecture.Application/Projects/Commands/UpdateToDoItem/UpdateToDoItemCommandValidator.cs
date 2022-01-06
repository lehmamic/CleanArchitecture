using FluentValidation;
using JetBrains.Annotations;

namespace CleanArchitecture.Application.Projects.Commands.UpdateToDoItem;

[UsedImplicitly]
public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.Description)
            .MaximumLength(100);
    }
}
