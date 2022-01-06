using System.Collections.Immutable;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using Dawn;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetToDoItems;

public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, IImmutableList<ToDoItemDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetToDoItemsQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<IImmutableList<ToDoItemDto>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new NotFoundException();
        }

        return project.Items
            .Where(i => request.Completed == null || request.Completed == i.IsDone)
            .Select(i => _mapper.Map<ToDoItemDto>(i))
            .ToImmutableList();
    }
}
