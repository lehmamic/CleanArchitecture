using AutoMapper;
using CleanArchitecture.Core.Projects;
using JetBrains.Annotations;

namespace CleanArchitecture.Application.Projects.Commands.CreateProject;

[UsedImplicitly]
public class CreateProjectCommandMappingProfile : Profile
{
    public CreateProjectCommandMappingProfile()
    {
        CreateMap<CreateProjectCommand, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Events, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore());
    }
}
