using System.Net;
using CleanArchitecture.Application.Projects.Dtos;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.SharedKernel.Extensions;
using CleanArchitecture.Testing.IntegrationTests.Utils;
using CleanArchitecture.Testing.Support.Fakers.Projects;
using CleanArchitecture.Testing.Support.Fakers.Projects.Entities;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CleanArchitecture.Testing.IntegrationTests.Projects.Queries;

[Collection(nameof(IntegrationTestCollection))]
public class GetProjectByIdTest : IntegrationTestBase
{
    public GetProjectByIdTest(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task GetProject_WithExistingProjectId_ReturnsProject()
    {
        // arrange
        var context = Services.GetRequiredService<AppDbContext>();

        var projects = new ProjectFaker().Generate(2);
        await context.Projects.AddRangeAsync(projects);

        await context.SaveChangesAsync();

        // act
        var actual = await Http.GetFromJsonAsync<ProjectDto>($"/api/v1/projects/{projects[0].Id}");

        // assert
        actual.Should().NotBeNull();
        actual!.Name.Should().Be(projects[0].Name);
    }

    [Fact]
    public async Task GetProject_WithNotExistingProjectId_ReturnsStatusCodeNotFound()
    {
        // arrange
        var projectId = Guid.NewGuid();

        // act
        var actual = await Http.GetAsync($"/api/v1/projects/{projectId}".ToUri());

        // assert
        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
