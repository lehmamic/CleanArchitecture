using CleanArchitecture.Application.Common.Emails;
using CleanArchitecture.Core.Projects.Events;
using Dawn;
using JetBrains.Annotations;
using MediatR;

namespace CleanArchitecture.Application.Projects.Events;

[UsedImplicitly]
public class ItemCompletedEmailNotificationHandler : INotificationHandler<ToDoItemCompletedEvent>
{
    private readonly IEmailSender _emailSender;

    public ItemCompletedEmailNotificationHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task Handle(ToDoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        Guard.Argument(notification, nameof(notification)).NotNull();

        return _emailSender.SendEmailAsync("test@test.com", "test@test.com", $"{notification.CompletedItem.Title} was completed.", notification.CompletedItem.ToString(), cancellationToken);
    }
}
