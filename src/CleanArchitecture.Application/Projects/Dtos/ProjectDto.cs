namespace CleanArchitecture.Application.Projects.Dtos;

public class ProjectDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<ToDoItemDto> Items { get; set; } = new();
}
