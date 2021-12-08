using System.Collections.Immutable;
using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetToDoItems;

public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, Result<IImmutableList<ToDoItemDto>>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetToDoItemsQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Result<IImmutableList<ToDoItemDto>>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result<IImmutableList<ToDoItemDto>>.NotFound();
        }

        return project.Items
            .Where(i => request.Completed == null || request.Completed == i.IsDone)
            .Select(i => _mapper.Map<ToDoItemDto>(i))
            .ToImmutableList();
    }
}
