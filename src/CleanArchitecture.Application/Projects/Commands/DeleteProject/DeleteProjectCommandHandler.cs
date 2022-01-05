using CleanArchitecture.Core.Projects;
using Dawn;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _repository;

    public DeleteProjectCommandHandler(IProjectRepository repository)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var project = await _repository.GetProjectByIdAsync(request.Id, cancellationToken);
        if (project is not null)
        {
            await _repository.DeleteAsync(project, cancellationToken);
        }
        
        return Unit.Value;
    }
}
