using System.Collections.Immutable;
using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetProjects;

public class GetProjectsQuery : IRequest<IImmutableList<ProjectDto>>
{
}
