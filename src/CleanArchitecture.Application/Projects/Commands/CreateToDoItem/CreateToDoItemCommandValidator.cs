using FluentValidation;

namespace CleanArchitecture.Application.Projects.Commands.CreateToDoItem;

public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(c => c.Description)
            .MaximumLength(200);
    }
}
