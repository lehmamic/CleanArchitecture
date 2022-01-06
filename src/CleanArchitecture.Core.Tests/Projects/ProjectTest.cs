using CleanArchitecture.Core.Projects;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Core.Tests.Projects;

public class ProjectTest
{
    [Fact]
    public void Constructor_WithName_InitializesName()
    {
        // act
        var project = CreateProject();

        // assert
        project.Name.Should().Be("test name");
    }

    [Fact]
    public void Constructor_WithName_InitializesTaskListToEmptyList()
    {
        // act
        var project = CreateProject();

        // assert
        project.Items.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithName_InitializesStatusToInProgress()
    {
        // act
        var project = CreateProject();

        // assert
        project.Status.Should().Be(ProjectStatus.Complete);
    }

    [Fact]
    public void AddItem_AddsItemToItems()
    {
        // arrange
        var project = CreateProject();
        var testItem = new ToDoItem();
        testItem.UpdateTitle("title");
        testItem.UpdateDescription("description");

        // act
        project.AddItem(testItem);

        // assert
        project.Items.Should().Contain(testItem);
    }

    [Fact]
    public void ThrowsExceptionGivenNullItem()
    {
        // arrange
        var project = CreateProject();

        // act
        var action = () => project.AddItem(null!);

        // assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("newItem");
    }

    private static Project CreateProject()
    {
        return new Project("test name");
    }
}
