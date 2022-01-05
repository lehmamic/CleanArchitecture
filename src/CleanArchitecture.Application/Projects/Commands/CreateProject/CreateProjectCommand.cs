using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<ProjectDto>
{
    public string Name { get; set; } = string.Empty;
}
