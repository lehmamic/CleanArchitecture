using FluentValidation;

namespace CleanArchitecture.Application.Projects.Commands.UpdateToDoItem;

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
