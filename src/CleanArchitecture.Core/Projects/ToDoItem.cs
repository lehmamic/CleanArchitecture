using CleanArchitecture.Core.Projects.Events;
using CleanArchitecture.SharedKernel.Models;
using Dawn;

namespace CleanArchitecture.Core.Projects;

public class ToDoItem : BaseEntity<Guid>
{
    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public bool IsDone { get; private set; }

    public ToDoItem UpdateTitle(string title)
    {
        Title = Guard.Argument(title, nameof(title)).NotNull();

        return this;
    }

    public ToDoItem UpdateDescription(string description)
    {
        Description = Guard.Argument(description, nameof(description)).NotNull();

        return this;
    }

    public ToDoItem MarkComplete()
    {
        if (!IsDone)
        {
            IsDone = true;
            Events.Add(new ToDoItemCompletedEvent(this));
        }

        return this;
    }

    public override string ToString()
    {
        string status = IsDone ? "Done!" : "Not done.";
        return $"{Id}: Status: {status} - {Title} - {Description}";
    }
}
