using Ardalis.Result;
using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.CreateToDoItem;

public class CreateToDoItemCommand : IRequest<Result<ToDoItemDto>>
{
    public Guid ProjectId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
