using AutoMapper;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Core.Projects;
using CleanArchitecture.SharedKernel.Exceptions;
using Dawn;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = Guard.Argument(repository, nameof(repository)).NotNull().Value;
        _mapper = Guard.Argument(mapper, nameof(mapper)).NotNull().Value;
    }

    public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdAsync(request.Id, cancellationToken);
        if (project is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<ProjectDto>(project);
    }
}
