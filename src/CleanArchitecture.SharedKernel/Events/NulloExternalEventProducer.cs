namespace CleanArchitecture.SharedKernel.Events;

public class NulloExternalEventProducer : IExternalEventProducer
{
    public Task PublishAsync(IExternalEvent @event, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
