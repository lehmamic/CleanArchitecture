namespace CleanArchitecture.Core.Projects;

public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetProjectsAsync(CancellationToken cancellationToken = default);

    Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default);

    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken = default);

    Task DeleteAsync(Project project, CancellationToken cancellationToken = default);
}
