using AutoBogus;
using CleanArchitecture.Application.Projects.Commands.CreateToDoItem;

namespace CleanArchitecture.Testing.Support.Fakers.Projects.Commands;

public sealed class CreateToDoItemCommandFaker : AutoFaker<CreateToDoItemCommand>
{
    public CreateToDoItemCommandFaker()
    {
        RuleFor(c => c.Title, f => f.Random.String(1, 10));
        RuleFor(c => c.Description, f => f.Random.String(1, 10));
    }
}
