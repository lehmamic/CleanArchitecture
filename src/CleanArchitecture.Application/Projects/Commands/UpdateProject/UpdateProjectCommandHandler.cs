using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<ProjectDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Result<ProjectDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.Id, cancellationToken);
        if (project is null)
        {
            return Result<ProjectDto>.NotFound();
        }

        project.UpdateName(request.Name);

        await _repository.UpdateProjectAsync(project, cancellationToken);

        return _mapper.Map<ProjectDto>(project);
    }
}
