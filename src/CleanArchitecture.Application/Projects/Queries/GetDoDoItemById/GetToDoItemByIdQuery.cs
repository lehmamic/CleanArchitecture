using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetDoDoItemById;

public class GetToDoItemByIdQuery : IRequest<ToDoItemDto>
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }
}
