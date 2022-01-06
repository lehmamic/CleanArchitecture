using AutoMapper;
using CleanArchitecture.Core.Projects;
using JetBrains.Annotations;

namespace CleanArchitecture.Application.Projects.Dtos;

[UsedImplicitly]
public class ProjectDtoMappingProfile : Profile
{
    public ProjectDtoMappingProfile()
    {
        CreateMap<Project, ProjectDto>();
    }
}
