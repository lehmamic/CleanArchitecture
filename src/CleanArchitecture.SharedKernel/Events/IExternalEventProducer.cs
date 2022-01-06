namespace CleanArchitecture.SharedKernel.Events;

public interface IExternalEventProducer
{
    Task PublishAsync(IExternalEvent @event, CancellationToken cancellationToken = default);
}
