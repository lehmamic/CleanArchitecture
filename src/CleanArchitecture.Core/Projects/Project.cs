using Ardalis.GuardClauses;
using CleanArchitecture.Core.Projects.Events;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Models;

namespace CleanArchitecture.Core.Projects;

public class Project : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<ToDoItem> _items = new();

    public Project(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }

    public string Name { get; private set; }

    public IEnumerable<ToDoItem> Items => _items.AsReadOnly();

    public ProjectStatus Status => _items.All(i => i.IsDone) ? ProjectStatus.Complete : ProjectStatus.InProgress;

    public void AddItem(ToDoItem newItem)
    {
        Guard.Against.Null(newItem, nameof(newItem));

        _items.Add(newItem);

        var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
        Events.Add(newItemAddedEvent);
    }
    
    public void RemoveItem(ToDoItem deletedItem)
    {
        Guard.Against.Null(deletedItem, nameof(deletedItem));

        _items.Remove(deletedItem);

        var itemRemovedEvent = new ItemRemovedEvent(this, deletedItem);
        Events.Add(itemRemovedEvent);
    }

    public void UpdateName(string newName)
    {
        Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    }
}
