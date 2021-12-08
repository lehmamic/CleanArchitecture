using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using CleanArchitecture.Application.Projects.Commands.CreateProject;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.CreateToDoItem;

public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Result<ToDoItemDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public CreateToDoItemCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<Result<ToDoItemDto>> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            return Result<ToDoItemDto>.NotFound();
        }

        var todoItem = _mapper.Map<ToDoItem>(request);
        project.AddItem(todoItem);

        await _repository.UpdateProjectAsync(project, cancellationToken);

        return _mapper.Map<ToDoItemDto>(todoItem);
    }
}
