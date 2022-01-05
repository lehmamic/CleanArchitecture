using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using CleanArchitecture.SharedKernel.Exceptions;
using Dawn;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var project = await _repository.GetProjectByIdAsync(request.Id, cancellationToken);
        if (project is null)
        {
            throw new NotFoundException();
        }

        project.UpdateName(request.Name);

        await _repository.UpdateProjectAsync(project, cancellationToken);

        return _mapper.Map<ProjectDto>(project);
    }
}
