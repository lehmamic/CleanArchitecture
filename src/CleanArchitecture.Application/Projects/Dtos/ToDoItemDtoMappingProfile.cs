using AutoMapper;
using CleanArchitecture.Core.Projects;
using JetBrains.Annotations;

namespace CleanArchitecture.Application.Projects.Dtos;

[UsedImplicitly]
public class ToDoItemDtoMappingProfile : Profile
{
    public ToDoItemDtoMappingProfile()
    {
        CreateMap<ToDoItem, ToDoItemDto>();
    }
}
