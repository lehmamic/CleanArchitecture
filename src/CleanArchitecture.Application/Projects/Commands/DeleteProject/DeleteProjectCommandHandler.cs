using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Commands.UpdateProject;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _repository;

    public DeleteProjectCommandHandler(IProjectRepository repository)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.Id, cancellationToken);
        if (project is not null)
        {
            await _repository.DeleteAsync(project, cancellationToken);
        }
        
        return Unit.Value;
    }
}
