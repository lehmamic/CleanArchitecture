using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<ProjectDto>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
