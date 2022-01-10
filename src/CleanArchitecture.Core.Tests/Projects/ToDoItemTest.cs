using CleanArchitecture.Core.Projects.Events;
using CleanArchitecture.Testing.Support.Fakers.Projects;
using CleanArchitecture.Testing.Support.Fakers.Projects.Entities;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Core.Tests.Projects;

public class ToDoItemTest
{
    [Fact]
    public void MarkComplete_WithStatusIsNotDone_SetsIsDoneToTrue()
    {
        // arrange
        var item = new ToDoItemFaker()
            .Generate();

        // act
        item.MarkComplete();

        // arrange
        item.IsDone.Should().BeTrue();
    }

    [Fact]
    public void MarkComplete_WithStatusIsNotDone_RaisesToDoItemCompletedEvent()
    {
        // arrange
        var item = new ToDoItemFaker()
            .Generate();

        // act
        item.MarkComplete();

        // arrange
        item.Events.Should().ContainSingle();
        item.Events.First().Should().BeOfType<ToDoItemCompletedEvent>();
    }
}
