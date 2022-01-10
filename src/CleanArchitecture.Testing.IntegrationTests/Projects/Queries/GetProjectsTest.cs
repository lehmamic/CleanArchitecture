using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Testing.IntegrationTests.Utils;
using CleanArchitecture.Testing.Support.Fakers.Projects;
using CleanArchitecture.Testing.Support.Fakers.Projects.Entities;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CleanArchitecture.Testing.IntegrationTests.Projects.Queries;

[Collection(nameof(IntegrationTestCollection))]
public class GetProjectsTest : IntegrationTestBase
{
    public GetProjectsTest(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task GetProjects_ReturnsProjects()
    {
        // arrange
        var context = Services.GetRequiredService<AppDbContext>();

        var projects = new ProjectFaker().Generate(2);
        await context.Projects.AddRangeAsync(projects);

        await context.SaveChangesAsync();

        // act
        var actual = await Http.GetFromJsonAsync<ProjectDto[]>("/api/v1/projects");

        // assert
        actual.Should().NotBeNull();
        actual.Should().HaveCount(2);
        actual.Should().Contain(p => string.Equals(p.Name, projects[0].Name, StringComparison.Ordinal));
        actual.Should().Contain(p => string.Equals(p.Name, projects[1].Name, StringComparison.Ordinal));
    }
}
