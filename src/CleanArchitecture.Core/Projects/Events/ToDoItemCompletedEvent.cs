using CleanArchitecture.SharedKernel.Events;
using CleanArchitecture.SharedKernel.Models;

namespace CleanArchitecture.Core.Projects.Events;

public class ToDoItemCompletedEvent : BaseDomainEvent
{
    public ToDoItemCompletedEvent(ToDoItem completedItem)
    {
        CompletedItem = completedItem;
    }

    public ToDoItem CompletedItem { get; set; }
}
