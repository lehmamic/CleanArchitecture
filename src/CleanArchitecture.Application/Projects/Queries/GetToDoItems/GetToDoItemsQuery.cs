using System.Collections.Immutable;
using Ardalis.Result;
using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetToDoItems;

public class GetToDoItemsQuery : IRequest<Result<IImmutableList<ToDoItemDto>>>
{
    public Guid ProjectId { get; set; }

    public bool? Completed { get; set; }
}
