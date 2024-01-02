using System.Text;
using Microsoft.Extensions.Options;
using N90.Application.Common.EventBus.Brokers;
using N90.Application.Common.Serializers;
using N90.Domain.Common.Events;
using N90.Infrastructure.Common.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace N90.Infrastructure.Common.EventBus.Services;

public abstract class EventSubscriber<TEvent> : IEventSubscriber where TEvent : Event
{
    private readonly EventBusSubscriberSettings _eventBusSubscriberSettings;
    private readonly JsonSerializerSettings _jsonSerializerSettings;
    private readonly IEnumerable<string> _queueNames;
    private readonly IRabbitMqConnectionProvider _rabbitMqConnectionProvider;
    private IEnumerable<EventingBasicConsumer> _consumer = default!;
    protected IChannel Channel = default!;

    public EventSubscriber(
        IRabbitMqConnectionProvider rabbitMqConnectionProvider,
        IOptions<EventBusSubscriberSettings> eventBusSubscriberSettings,
        IEnumerable<string> queueNames,
        IJsonSerializationSettingsProvider jsonSerializationSettingsProvider
        )
    {
        _rabbitMqConnectionProvider = rabbitMqConnectionProvider;
        _eventBusSubscriberSettings = eventBusSubscriberSettings.Value;
        _queueNames = queueNames;

        _jsonSerializerSettings = jsonSerializationSettingsProvider.Get(true);
        _jsonSerializerSettings.ContractResolver = new DefaultContractResolver();
        _jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
    }

    public ValueTask StartAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public ValueTask StopAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    protected virtual async ValueTask SetChannelAsync(CancellationToken token)
    {
        Channel = await _rabbitMqConnectionProvider.CreateChannelAsync(token);
        await Channel.BasicQosAsync(0, _eventBusSubscriberSettings.PrefetchCount, false);
    }

    protected virtual async ValueTask SetConsumerAsync(CancellationToken cancellationToken)
    {
        _consumer = await Task.WhenAll(
            _queueNames.Select(
                async queueName =>
                {
                    var consumer = new EventingBasicConsumer(Channel);
                    consumer.Received += async (sender, args) => await HandleInternalAsync(sender, args, cancellationToken);
                    await Channel.BasicConsumeAsync(queueName, false, consumer);
                    return consumer;
                }
            )
        );
    }

    protected virtual async ValueTask HandleInternalAsync(object? sender, BasicDeliverEventArgs ea, CancellationToken cancellationToken = default)
    {
        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
        var @event = JsonConvert.DeserializeObject<TEvent>(message, _jsonSerializerSettings);
        @event.Redelivered = ea.Redelivered;
        var result = await ProcessAsync(@event, cancellationToken);

        if (result.Result)
            await Channel.BasicAckAsync(ea.DeliveryTag, false);
        else
            await Channel.BasicNackAsync(ea.DeliveryTag, false, result.Redeliver);

    }

    protected abstract ValueTask<(bool Result, bool Redeliver)> ProcessAsync(TEvent @event, CancellationToken cancellationToken);
}
