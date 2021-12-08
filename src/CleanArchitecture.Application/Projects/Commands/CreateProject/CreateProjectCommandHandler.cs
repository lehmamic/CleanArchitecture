using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<ProjectDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Result<ProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = _mapper.Map<Project>(request);
        var resultingProject = await _repository.AddProjectAsync(project, cancellationToken);

        return _mapper.Map<ProjectDto>(resultingProject);
    }
}
