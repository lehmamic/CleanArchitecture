using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.DeleteToDoItem;

public class DeleteToDoItemCommand : IRequest
{
    public Guid ProjectId { get; set; }

    public Guid Id { get; set; }
}
