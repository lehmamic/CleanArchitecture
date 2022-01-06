using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using Dawn;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetDoDoItemById;

public class GetToDoItemByIdQueryHandler : IRequestHandler<GetToDoItemByIdQuery, ToDoItemDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetToDoItemByIdQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ToDoItemDto> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
    {
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

        return _mapper.Map<ToDoItemDto>(toDoItem);
    }
}
