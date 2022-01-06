using AutoBogus;
using CleanArchitecture.Core.Projects;

namespace CleanArchitecture.TestingSupport.Fakers.Projects;

public sealed class ToDoItemFaker : AutoFaker<ToDoItem>
{
    public ToDoItemFaker()
    {
        RuleFor(nameof(ToDoItem.Title), f => f.Random.String(1, 10));
        RuleFor(nameof(ToDoItem.Description), f => f.Random.String(1, 10));
        RuleFor(nameof(ToDoItem.IsDone), _ => false);
    }
}
