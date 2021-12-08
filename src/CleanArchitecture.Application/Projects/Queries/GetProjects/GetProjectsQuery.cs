using System.Collections.Immutable;
using Ardalis.Result;
using CleanArchitecture.Application.Projects.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Projects.Queries.GetProjects;

public class GetProjectsQuery : IRequest<Result<IImmutableList<ProjectDto>>>
{
}
