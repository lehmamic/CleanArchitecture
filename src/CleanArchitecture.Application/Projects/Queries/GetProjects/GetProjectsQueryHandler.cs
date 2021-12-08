using System.Collections.Immutable;
using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetProjects;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, Result<IImmutableList<ProjectDto>>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Result<IImmutableList<ProjectDto>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetProjectsAsync(cancellationToken);
        return projects.Select(p => _mapper.Map<ProjectDto>(p))
            .ToImmutableList();
    }
}
