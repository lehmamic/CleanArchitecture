using Ardalis.Result;
using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<Result<ProjectDto>>
{
    public Guid Id { get; set; }
}
