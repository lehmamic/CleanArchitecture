using AutoMapper;
using CleanArchitecture.Core.Projects;

namespace CleanArchitecture.Application.Projects.Dtos;

public class ProjectDtoMappingProfile : Profile
{
    public ProjectDtoMappingProfile()
    {
        CreateMap<Project, ProjectDto>();
    }
}
