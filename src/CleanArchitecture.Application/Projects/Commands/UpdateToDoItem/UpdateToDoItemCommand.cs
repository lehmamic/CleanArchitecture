using Ardalis.Result;
using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.UpdateToDoItem;

public class UpdateToDoItemCommand : IRequest<Result<ToDoItemDto>>
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
