using Ardalis.GuardClauses;
using CleanArchitecture.Core.Projects.Events;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core.Projects;

public class ToDoItem : BaseEntity<Guid>
{
    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public bool IsDone { get; private set; }

    public void UpdateTitle(string title)
    {
        Title = Guard.Against.NullOrEmpty(title, nameof(title));
    }
    
    public void UpdateDescription(string description)
    {
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
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
