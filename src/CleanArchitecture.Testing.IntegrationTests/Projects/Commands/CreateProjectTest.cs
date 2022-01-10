using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Testing.IntegrationTests.Utils;
using CleanArchitecture.Testing.Support.Fakers.Projects.Commands;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CleanArchitecture.Testing.IntegrationTests.Projects.Commands;

[Collection(nameof(IntegrationTestCollection))]
public class CreateProjectTest : IntegrationTestBase
{
    public CreateProjectTest(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task CreateProjects_WithValidCreateProjectCommand_CreatesProject()
    {
        // arrange
        var command = new CreateProjectCommandFaker().Generate();

        // act
        await Http.PostAsJsonAsync("/api/v1/projects", command);

        // assert
        var context = Services.GetRequiredService<AppDbContext>();
        var projects = await context.Projects.ToListAsync();

        projects.Should().HaveCount(1);
        projects[0].Name.Should().Be(command.Name);
    }
}
