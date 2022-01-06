using MediatR;

namespace CleanArchitecture.Infrastructure.Events;

// Do we still need that? MediatR doesn't seem to require that on publish
public class EventEnvelop<TEvent> : INotification
{
    public TEvent Event { get; init; } = default!;
}
