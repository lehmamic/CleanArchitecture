using AutoMapper;
using CleanArchitecture.Core.Projects;

namespace CleanArchitecture.Application.Projects.Dtos;

public class ToDoItemDtoMappingProfile : Profile
{
    public ToDoItemDtoMappingProfile()
    {
        CreateMap<ToDoItem, ToDoItemDto>();
    }
}
