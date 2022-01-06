using CleanArchitecture.Application.Common.Emails;
using CleanArchitecture.Application.Projects.Events;
using CleanArchitecture.Core.Projects;
using CleanArchitecture.Core.Projects.Events;
using FluentAssertions;
using NSubstitute;
using NSubstituteAutoMocker.Standard;
using Xunit;

namespace CleanArchitecture.Application.Tests.Projects.Events;

public class ItemCompletedEmailNotificationHandlerTest
{
    private readonly NSubstituteAutoMocker<ItemCompletedEmailNotificationHandler> _autoMocker;

    public ItemCompletedEmailNotificationHandlerTest()
    {
        _autoMocker = new NSubstituteAutoMocker<ItemCompletedEmailNotificationHandler>();
    }

    [Fact]
    public async Task Handle_WithNotificationIsNull_ThrowsException()
    {
        // arrange
        var handler = _autoMocker.ClassUnderTest;

        // act
        var action = () => handler.Handle(null!, CancellationToken.None);

        // assert
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SendsEmailGivenEventInstance()
    {
        // arrange
        var handler = _autoMocker.ClassUnderTest;

        // act
        await handler.Handle(new ToDoItemCompletedEvent(new ToDoItem()), CancellationToken.None);

        _autoMocker.Get<IEmailSender>().ReceivedWithAnyArgs();
    }
}
