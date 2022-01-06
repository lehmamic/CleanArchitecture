using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using Dawn;
using JetBrains.Annotations;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands.UpdateToDoItem;

[UsedImplicitly]
public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, ToDoItemDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public UpdateToDoItemCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ToDoItemDto> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var project = await _repository.GetProjectByIdAsync(request.ProjectId, cancellationToken);
        if (project is null)
        {
            throw new NotFoundException();
        }

        var toDoItem = project.Items.FirstOrDefault(i => i.Id == request.Id);
        if (toDoItem is null)
        {
            throw new NotFoundException();
        }

        toDoItem.UpdateTitle(request.Title);
        toDoItem.UpdateDescription(request.Description);

        await _repository.UpdateProjectAsync(project, cancellationToken);

        return _mapper.Map<ToDoItemDto>(toDoItem);
    }
}
