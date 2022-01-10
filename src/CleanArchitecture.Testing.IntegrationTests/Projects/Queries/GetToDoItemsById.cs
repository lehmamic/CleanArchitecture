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
public class GetToDoItemByIdTest : IntegrationTestBase
{
    public GetToDoItemByIdTest(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task GetToDoItem_WithExistingItem_ReturnsToDoItem()
    {
        // arrange
        var context = Services.GetRequiredService<AppDbContext>();

        var project = new ProjectFaker().Generate();
        new ToDoItemFaker().Generate(2).ForEach(i => project.AddItem(i));

        await context.Projects.AddAsync(project);

        await context.SaveChangesAsync();

        // act
        var actual = await Http.GetFromJsonAsync<ToDoItemDto>($"/api/v1/projects/{project.Id}/items/{project.Items.ElementAt(0).Id}");

        // assert
        actual.Should().NotBeNull();
        actual!.Title.Should().Be(project.Items.ElementAt(0).Title);
    }

    [Fact]
    public async Task GetToDoItem_WithNotExistingItem_ReturnsStatusCodeNotFound()
    {
        // arrange
        var context = Services.GetRequiredService<AppDbContext>();

        var project = new ProjectFaker().Generate();
        await context.Projects.AddAsync(project);

        var itemId = Guid.NewGuid();

        await context.SaveChangesAsync();

        // act
        var actual = await Http.GetAsync($"/api/v1/projects/{project.Id}/items/{itemId}".ToUri());

        // assert
        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetToDoItem_WithNotExistingProject_ReturnsStatusCodeNotFound()
    {
        // arrange
        var projectId = Guid.NewGuid();
        var itemId = Guid.NewGuid();

        // act
        var actual = await Http.GetAsync($"/api/v1/projects/{projectId}/items/{itemId}".ToUri());

        // assert
        actual.Should().NotBeNull();
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
