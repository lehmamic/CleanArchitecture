using System.Collections.Immutable;
using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using Dawn;
using JetBrains.Annotations;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetProjects;

[UsedImplicitly]
public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IImmutableList<ProjectDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<IImmutableList<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetProjectsAsync(cancellationToken);
        return projects.Select(p => _mapper.Map<ProjectDto>(p))
            .ToImmutableList();
    }
}
