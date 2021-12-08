using Ardalis.GuardClauses;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.DeleteToDoItem;

public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Unit>
{
    private readonly IProjectRepository _repository;

    public DeleteToDoItemCommandHandler(IProjectRepository repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<Unit> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Unit.Value;
        }

        var toDoItem = project.Items.FirstOrDefault(i => i.Id == request.Id);
        if (toDoItem is not null)
        {
            project.RemoveItem(toDoItem);
        }

        await _repository.UpdateProjectAsync(project, cancellationToken);

        return Unit.Value;
    }
}
