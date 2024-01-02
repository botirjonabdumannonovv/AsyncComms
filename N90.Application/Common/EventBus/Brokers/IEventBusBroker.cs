using N90.Domain.Common.Events;

namespace N90.Application.Common.EventBus.Brokers;

public interface IEventBusBroker
{
    ValueTask PublishAsync<TEvent>(TEvent @event, string exchange, string routingKey, CancellationToken cancellationToken = default) where TEvent : Event;
}