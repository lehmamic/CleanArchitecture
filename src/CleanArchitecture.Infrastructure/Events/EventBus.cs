using CleanArchitecture.SharedKernel.Events;
using Dawn;
using MediatR;

namespace CleanArchitecture.Infrastructure.Events;

public class EventBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly IExternalEventProducer _externalEventProducer;

    public EventBus(IMediator mediator, IExternalEventProducer externalEventProducer)
    {
        this._mediator = Guard.Argument(mediator, nameof(mediator)).NotNull().Value;
        this._externalEventProducer = Guard.Argument(externalEventProducer, nameof(externalEventProducer)).NotNull().Value;
    }

    public async Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        await _mediator.Publish(@event, cancellationToken);

        if (@event is IExternalEvent externalEvent)
        {
            await _externalEventProducer.PublishAsync(externalEvent, cancellationToken);
        }
    }
}
