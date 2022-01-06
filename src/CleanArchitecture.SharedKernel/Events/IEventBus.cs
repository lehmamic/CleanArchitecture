namespace CleanArchitecture.SharedKernel.Events;

public interface IEventBus
{
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}
