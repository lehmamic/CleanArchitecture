using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.Id, cancellationToken);
        if (project is null)
        {
            return Result<ProjectDto>.NotFound();
        }

        return _mapper.Map<ProjectDto>(project);
    }
}
