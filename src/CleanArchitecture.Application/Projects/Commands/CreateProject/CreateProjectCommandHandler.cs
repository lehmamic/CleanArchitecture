using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using Dawn;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var project = _mapper.Map<Project>(request);
        var resultingProject = await _repository.AddProjectAsync(project, cancellationToken);

        return _mapper.Map<ProjectDto>(resultingProject);
    }
}
