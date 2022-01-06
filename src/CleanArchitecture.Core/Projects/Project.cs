using CleanArchitecture.Core.Projects.Events;
using CleanArchitecture.SharedKernel.Models;
using Dawn;

namespace CleanArchitecture.Core.Projects;

public class Project : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<ToDoItem> _items = new();

    public Project(string name)
    {
        Name = Guard.Argument(name, nameof(name)).NotNull();
    }

    public string Name { get; private set; }

    public IEnumerable<ToDoItem> Items => _items.AsReadOnly();

    public ProjectStatus Status => _items.All(i => i.IsDone) ? ProjectStatus.Complete : ProjectStatus.InProgress;

    public void AddItem(ToDoItem newItem)
    {
        Guard.Argument(newItem, nameof(newItem)).NotNull();

        _items.Add(newItem);

        var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
        Events.Add(newItemAddedEvent);
    }

    public void RemoveItem(ToDoItem deletedItem)
    {
        Guard.Argument(deletedItem, nameof(deletedItem)).NotNull();

        _items.Remove(deletedItem);

        var itemRemovedEvent = new ItemRemovedEvent(this, deletedItem);
        Events.Add(itemRemovedEvent);
    }

    public void UpdateName(string newName)
    {
        Name = Guard.Argument(newName, nameof(newName)).NotNull();
    }
}
