using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest
{
    public Guid Id { get; set; }
}
