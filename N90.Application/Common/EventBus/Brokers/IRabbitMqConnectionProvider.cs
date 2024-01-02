using RabbitMQ.Client;

namespace N90.Application.Common.EventBus.Brokers;

public interface IRabbitMqConnectionProvider
{
    ValueTask<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default);
}
    