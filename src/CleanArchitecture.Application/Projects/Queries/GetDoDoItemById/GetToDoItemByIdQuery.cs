using Ardalis.Result;
using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetDoDoItemById;

public class GetToDoItemByIdQuery : IRequest<Result<ToDoItemDto>>
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }
}
