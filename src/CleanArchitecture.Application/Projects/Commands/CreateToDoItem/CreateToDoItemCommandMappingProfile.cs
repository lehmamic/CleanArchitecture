using AutoMapper;
using CleanArchitecture.Application.Projects.Commands.CreateProject;
using CleanArchitecture.Core.Projects;
using JetBrains.Annotations;

namespace CleanArchitecture.Application.Projects.Commands.CreateToDoItem;

[UsedImplicitly]
public class CreateToDoItemCommandMappingProfile : Profile
{
    public CreateToDoItemCommandMappingProfile()
    {
        CreateMap<CreateToDoItemCommand, ToDoItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Events, opt => opt.Ignore())
            .ForMember(dest => dest.IsDone, opt => opt.Ignore());
    }
}
