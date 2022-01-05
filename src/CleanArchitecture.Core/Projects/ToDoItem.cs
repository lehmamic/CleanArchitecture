using CleanArchitecture.Core.Projects.Events;
using CleanArchitecture.SharedKernel.Models;
using Dawn;

namespace CleanArchitecture.Core.Projects;

public class ToDoItem : BaseEntity<Guid>
{
    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public bool IsDone { get; private set; }

    public void UpdateTitle(string title)
    {
        Title = Guard.Argument(title, nameof(title)).NotNull();
    }
    
    public void UpdateDescription(string description)
    {
        Description = Guard.Argument(description, nameof(description)).NotNull();
    }

    public void MarkComplete()
    {
        if (!IsDone)
        {
            IsDone = true;
            Events.Add(new ToDoItemCompletedEvent(this));
        }
    }

    public override string ToString()
    {
        string status = IsDone ? "Done!" : "Not done.";
        return $"{Id}: Status: {status} - {Title} - {Description}";
    }
}
