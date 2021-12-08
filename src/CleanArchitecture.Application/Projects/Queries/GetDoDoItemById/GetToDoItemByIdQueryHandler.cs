using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetDoDoItemById;

public class GetToDoItemByIdQueryHandler : IRequestHandler<GetToDoItemByIdQuery, Result<ToDoItemDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetToDoItemByIdQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Result<ToDoItemDto>> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result<ToDoItemDto>.NotFound();
        }

        var toDoItem = project.Items.FirstOrDefault(i => i.Id == request.Id);
        if (toDoItem is null)
        {
            return Result<ToDoItemDto>.NotFound();
        }

        return _mapper.Map<ToDoItemDto>(toDoItem);
    }
}
