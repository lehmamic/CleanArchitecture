using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using Dawn;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.CreateToDoItem;

public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, ToDoItemDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public CreateToDoItemCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ToDoItemDto> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new NotFoundException();
        }

        var todoItem = _mapper.Map<ToDoItem>(request);
        project.AddItem(todoItem);

        await _repository.UpdateProjectAsync(project, cancellationToken);

        return _mapper.Map<ToDoItemDto>(todoItem);
    }
}
