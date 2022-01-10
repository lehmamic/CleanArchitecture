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
public class GetToDoItemsTest : IntegrationTestBase
{
    public GetToDoItemsTest(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task GetToDoItems_WithExistingProject_ReturnsToDoItems()
    {
        // arrange
        var context = Services.GetRequiredService<AppDbContext>();

        var project = new ProjectFaker().Generate();
        new ToDoItemFaker().Generate(2).ForEach(i => project.AddItem(i));

        await context.Projects.AddAsync(project);

        await context.SaveChangesAsync();

        // act
        var actual = await Http.GetFromJsonAsync<ToDoItemDto[]>($"/api/v1/projects/{project.Id}/items");

        // assert
        actual.Should().NotBeNull();
        actual.Should().HaveCount(2);
        actual.Should().Contain(p => string.Equals(p.Title, project.Items.ElementAt(0).Title, StringComparison.Ordinal));
        actual.Should().Contain(p => string.Equals(p.Title, project.Items.ElementAt(1).Title, StringComparison.Ordinal));
    }

    [Fact]
    public async Task GetToDoItems_WithNotExistingProject_ReturnsStatusCodeNotFound()
    {
        // arrange
        var projectId = Guid.NewGuid();

        // act
        var actual = await Http.GetAsync($"/api/v1/projects/{projectId}/items".ToUri());

        // assert
        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
