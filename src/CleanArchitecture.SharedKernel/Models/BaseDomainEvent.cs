using CleanArchitecture.SharedKernel.Events;
using NodaTime;

namespace CleanArchitecture.SharedKernel.Models;

public abstract class BaseDomainEvent : IEvent
{
    public Instant Timestamp { get; protected set; } = SystemClock.Instance.GetCurrentInstant();
}
