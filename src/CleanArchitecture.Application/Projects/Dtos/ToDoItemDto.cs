namespace CleanArchitecture.Application.Projects.Dtos;

public class ToDoItemDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool IsDone { get; set; }
}
